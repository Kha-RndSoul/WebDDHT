using WebDDHT.Models;

namespace WebDDHT.ViewModels
{
    /// <summary>
    /// ViewModel cho trang xác nhận đơn hàng
    /// </summary>
    public class OrderConfirmViewModel
    {
        public Order Order { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        // Helper properties
        public string OrderStatusText
        {
            get
            {
                if (Order == null) return string.Empty;

                switch (Order.OrderStatus)
                {
                    case "PENDING":
                        return "Chờ xác nhận";
                    case "CONFIRMED":
                        return "Đã xác nhận";
                    case "SHIPPING":
                        return "Đang giao hàng";
                    case "DELIVERED":
                        return "Đã giao hàng";
                    case "CANCELLED":
                        return "Đã hủy";
                    default:
                        return Order.OrderStatus;
                }
            }
        }

        public string PaymentMethodText
        {
            get
            {
                if (Order == null) return string.Empty;

                switch (Order.PaymentMethod)
                {
                    case "COD":
                        return "Thanh toán khi nhận hàng (COD)";
                    case "BANK_TRANSFER":
                        return "Chuyển khoản ngân hàng";
                    case "MOMO":
                        return "Ví MoMo";
                    case "VNPAY":
                        return "VNPay";
                    case "ZALOPAY":
                        return "ZaloPay";
                    default:
                        return Order.PaymentMethod;
                }
            }
        }
    }
}