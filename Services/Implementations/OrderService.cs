using System;
using System.Collections.Generic;
using System.Linq;
using WebDDHT.Models;
using WebDDHT.Repositories;
using WebDDHT.Services.Interfaces;

namespace WebDDHT.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Order> GetCustomerOrders(int customerId)
        {
            return _unitOfWork.Orders.GetByCustomer(customerId);
        }

        public Order GetOrderById(int orderId)
        {
            return _unitOfWork.Orders.GetById(orderId);
        }

        public Order GetOrderByCode(string orderCode)
        {
            return _unitOfWork.Orders.GetByOrderCode(orderCode);
        }

        public Order GetOrderWithDetails(int orderId)
        {
            return _unitOfWork.Orders.GetOrderWithDetails(orderId);
        }

        public bool CreateOrderFromCart(
            int customerId,
            string shippingName,
            string shippingPhone,
            string shippingAddress,
            string paymentMethod,
            string couponCode,
            string note,
            out int orderId,
            out string errorMessage)
        {
            orderId = 0;
            errorMessage = string.Empty;

            try
            {
                // Begin transaction
                _unitOfWork.BeginTransaction();

                // Validate cart
                var cartItems = _unitOfWork.Cart.GetByCustomer(customerId).ToList();
                if (!cartItems.Any())
                {
                    errorMessage = "Giỏ hàng trống.";
                    return false;
                }

                // Validate stock for all items
                foreach (var item in cartItems)
                {
                    var product = _unitOfWork.Products.GetById(item.ProductId);
                    if (product == null || !product.IsActive)
                    {
                        errorMessage = $"Sản phẩm '{item.Product?.ProductName}' không còn kinh doanh.";
                        _unitOfWork.Rollback();
                        return false;
                    }

                    if (product.StockQuantity < item.Quantity)
                    {
                        errorMessage = $"Sản phẩm '{product.ProductName}' chỉ còn {product.StockQuantity} trong kho.";
                        _unitOfWork.Rollback();
                        return false;
                    }
                }

                // Calculate total
                decimal subtotal = cartItems.Sum(c => c.Product.GetCurrentPrice() * c.Quantity);
                decimal discount = 0;
                int? couponId = null;

                // Apply coupon if provided
                if (!string.IsNullOrWhiteSpace(couponCode))
                {
                    string couponError;
                    if (!_unitOfWork.Coupons.ValidateCoupon(couponCode, subtotal, out couponError))
                    {
                        errorMessage = couponError;
                        _unitOfWork.Rollback();
                        return false;
                    }

                    var coupon = _unitOfWork.Coupons.GetByCode(couponCode);
                    if (coupon != null)
                    {
                        couponId = coupon.CouponId;

                        if (coupon.DiscountType == "PERCENTAGE")
                        {
                            discount = subtotal * (coupon.DiscountValue / 100);
                        }
                        else // FIXED_AMOUNT
                        {
                            discount = coupon.DiscountValue;
                        }

                        // Increment coupon used count
                        _unitOfWork.Coupons.IncrementUsedCount(coupon.CouponId);
                    }
                }

                decimal totalAmount = subtotal - discount;

                // Generate order code
                string orderCode = _unitOfWork.Orders.GenerateOrderCode();

                // Create order
                var order = new Order
                {
                    CustomerId = customerId,
                    OrderCode = orderCode,
                    OrderStatus = "PENDING",
                    PaymentMethod = paymentMethod,
                    PaymentStatus = paymentMethod == "COD" ? "PENDING" : "UNPAID",
                    TotalAmount = totalAmount,
                    ShippingName = shippingName,
                    ShippingPhone = shippingPhone,
                    ShippingAddress = shippingAddress,
                    Note = note,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                _unitOfWork.Orders.Add(order);
                _unitOfWork.Complete(); // Save to get order ID

                // Create order details
                foreach (var item in cartItems)
                {
                    var product = _unitOfWork.Products.GetById(item.ProductId);
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        ProductName = product.ProductName,
                        UnitPrice = product.GetCurrentPrice(),
                        Quantity = item.Quantity,
                        Subtotal = product.GetCurrentPrice() * item.Quantity,
                        CreatedAt = DateTime.Now
                    };

                    // Thêm vào navigation property của Order
                    order.OrderDetails.Add(orderDetail);

                    // Update product stock and sold count
                    _unitOfWork.Products.UpdateStock(item.ProductId, -item.Quantity);
                    product.SoldCount += item.Quantity;
                    _unitOfWork.Products.Update(product);
                }

                // Add coupon usage if applied
                if (couponId.HasValue)
                {
                    var orderCoupon = new OrderCoupon
                    {
                        OrderId = order.Id,
                        CouponId = couponId.Value,
                        DiscountAmount = discount,
                        AppliedAt = DateTime.Now
                    };

                    //Thêm vào navigation property của Order
                    order.OrderCoupons.Add(orderCoupon);
                }

                // Clear cart
                _unitOfWork.Cart.ClearCart(customerId);

                // Commit transaction
                _unitOfWork.Commit();

                orderId = order.Id;
                return true;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                errorMessage = $"Lỗi khi tạo đơn hàng: {ex.Message}";
                return false;
            }
        }

        public bool UpdateOrderStatus(int orderId, string newStatus, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                var order = _unitOfWork.Orders.GetById(orderId);
                if (order == null)
                {
                    errorMessage = "Đơn hàng không tồn tại.";
                    return false;
                }

                // Validate status transition
                var validStatuses = new[] { "PENDING", "CONFIRMED", "SHIPPING", "DELIVERED", "CANCELLED" };
                if (!validStatuses.Contains(newStatus))
                {
                    errorMessage = "Trạng thái không hợp lệ.";
                    return false;
                }

                order.OrderStatus = newStatus;
                order.UpdatedAt = DateTime.Now;

                if (newStatus == "DELIVERED")
                {
                    order.PaymentStatus = "PAID";
                }

                _unitOfWork.Orders.Update(order);
                _unitOfWork.Complete();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi cập nhật trạng thái:  {ex.Message}";
                return false;
            }
        }

        public bool CancelOrder(int orderId, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                _unitOfWork.BeginTransaction();

                var order = _unitOfWork.Orders.GetOrderWithDetails(orderId);
                if (order == null)
                {
                    errorMessage = "Đơn hàng không tồn tại.";
                    _unitOfWork.Rollback();
                    return false;
                }

                // Can only cancel PENDING orders
                if (order.OrderStatus != "PENDING")
                {
                    errorMessage = "Chỉ có thể hủy đơn hàng đang chờ xử lý.";
                    _unitOfWork.Rollback();
                    return false;
                }

                // Restore stock
                foreach (var detail in order.OrderDetails)
                {
                    _unitOfWork.Products.UpdateStock(detail.ProductId, detail.Quantity);

                    var product = _unitOfWork.Products.GetById(detail.ProductId);
                    if (product != null)
                    {
                        product.SoldCount -= detail.Quantity;
                        _unitOfWork.Products.Update(product);
                    }
                }

                // Restore coupon usage
                var orderCoupon = order.OrderCoupons.FirstOrDefault();
                if (orderCoupon != null)
                {
                    var coupon = _unitOfWork.Coupons.GetById(orderCoupon.CouponId);
                    if (coupon != null)
                    {
                        coupon.UsedCount--;
                        _unitOfWork.Coupons.Update(coupon);
                    }
                }

                // Update order status
                order.OrderStatus = "CANCELLED";
                order.UpdatedAt = DateTime.Now;
                _unitOfWork.Orders.Update(order);

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                errorMessage = $"Lỗi khi hủy đơn hàng: {ex.Message}";
                return false;
            }
        }

        public decimal CalculateOrderTotal(int customerId, string couponCode, out decimal discount)
        {
            discount = 0;

            var cartItems = _unitOfWork.Cart.GetByCustomer(customerId).ToList();
            if (!cartItems.Any())
                return 0;

            decimal subtotal = cartItems.Sum(c => c.Product.GetCurrentPrice() * c.Quantity);

            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                string errorMessage;
                if (_unitOfWork.Coupons.ValidateCoupon(couponCode, subtotal, out errorMessage))
                {
                    var coupon = _unitOfWork.Coupons.GetByCode(couponCode);
                    if (coupon != null)
                    {
                        if (coupon.DiscountType == "PERCENTAGE")
                        {
                            discount = subtotal * (coupon.DiscountValue / 100);
                        }
                        else
                        {
                            discount = coupon.DiscountValue;
                        }
                    }
                }
            }

            return subtotal - discount;
        }
    }
}