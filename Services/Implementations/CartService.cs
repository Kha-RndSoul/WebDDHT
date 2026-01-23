using System;
using System.Collections.Generic;
using System.Linq;
using WebDDHT.Models;
using WebDDHT.Repositories;
using WebDDHT.Services.Interfaces;

namespace WebDDHT.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CartItem> GetCartItems(int customerId)
        {
            return _unitOfWork.Cart.GetByCustomer(customerId);
        }

        public bool AddToCart(int customerId, int productId, int quantity, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                // Validate quantity
                if (quantity <= 0)
                {
                    errorMessage = "Số lượng phải lớn hơn 0.";
                    return false;
                }

                // Check product exists and is active
                var product = _unitOfWork.Products.GetById(productId);
                if (product == null || !product.IsActive)
                {
                    errorMessage = "Sản phẩm không tồn tại hoặc đã ngừng kinh doanh.";
                    return false;
                }

                // Check stock availability
                if (product.StockQuantity < quantity)
                {
                    errorMessage = $"Chỉ còn {product.StockQuantity} sản phẩm trong kho.";
                    return false;
                }

                // Check if item already in cart
                var existingItem = _unitOfWork.Cart.GetCartItem(customerId, productId);

                if (existingItem != null)
                {
                    // Update quantity
                    int newQuantity = existingItem.Quantity + quantity;

                    // Check stock for new quantity
                    if (product.StockQuantity < newQuantity)
                    {
                        errorMessage = $"Chỉ còn {product.StockQuantity} sản phẩm trong kho.";
                        return false;
                    }

                    existingItem.Quantity = newQuantity;
                    existingItem.UpdatedAt = DateTime.Now;
                    _unitOfWork.Cart.Update(existingItem);
                }
                else
                {
                    // Add new item
                    var cartItem = new CartItem
                    {
                        CustomerId = customerId,
                        ProductId = productId,
                        Quantity = quantity,
                        AddedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    _unitOfWork.Cart.Add(cartItem);
                }

                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi thêm vào giỏ hàng: {ex.Message}";
                return false;
            }
        }

        public bool UpdateCartItemQuantity(int customerId, int productId, int quantity, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                if (quantity <= 0)
                {
                    errorMessage = "Số lượng phải lớn hơn 0.";
                    return false;
                }

                var cartItem = _unitOfWork.Cart.GetCartItem(customerId, productId);
                if (cartItem == null)
                {
                    errorMessage = "Sản phẩm không có trong giỏ hàng.";
                    return false;
                }

                // Check stock
                var product = _unitOfWork.Products.GetById(productId);
                if (product == null || product.StockQuantity < quantity)
                {
                    errorMessage = $"Chỉ còn {product?.StockQuantity ?? 0} sản phẩm trong kho.";
                    return false;
                }

                cartItem.Quantity = quantity;
                cartItem.UpdatedAt = DateTime.Now;
                _unitOfWork.Cart.Update(cartItem);
                _unitOfWork.Complete();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi cập nhật giỏ hàng: {ex.Message}";
                return false;
            }
        }

        public bool RemoveFromCart(int customerId, int productId, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                var cartItem = _unitOfWork.Cart.GetCartItem(customerId, productId);
                if (cartItem == null)
                {
                    errorMessage = "Sản phẩm không có trong giỏ hàng.";
                    return false;
                }

                _unitOfWork.Cart.Remove(cartItem);
                _unitOfWork.Complete();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi xóa khỏi giỏ hàng: {ex.Message}";
                return false;
            }
        }

        public bool ClearCart(int customerId, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                _unitOfWork.Cart.ClearCart(customerId);
                _unitOfWork.Complete();

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi xóa giỏ hàng: {ex.Message}";
                return false;
            }
        }

        public int GetCartItemCount(int customerId)
        {
            return _unitOfWork.Cart.CountItems(customerId);
        }

        public decimal GetCartTotal(int customerId)
        {
            return _unitOfWork.Cart.GetCartTotal(customerId);
        }

        public bool ValidateCart(int customerId, out string errorMessage)
        {
            errorMessage = string.Empty;

            try
            {
                var cartItems = _unitOfWork.Cart.GetByCustomer(customerId);

                if (!cartItems.Any())
                {
                    errorMessage = "Giỏ hàng trống.";
                    return false;
                }

                // Check each item stock
                foreach (var item in cartItems)
                {
                    var product = _unitOfWork.Products.GetById(item.ProductId);

                    if (product == null || !product.IsActive)
                    {
                        errorMessage = $"Sản phẩm '{item.Product?.ProductName}' không còn kinh doanh.";
                        return false;
                    }

                    if (product.StockQuantity < item.Quantity)
                    {
                        errorMessage = $"Sản phẩm '{product.ProductName}' chỉ còn {product.StockQuantity} trong kho.";
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Lỗi khi kiểm tra giỏ hàng: {ex.Message}";
                return false;
            }
        }
    }
}