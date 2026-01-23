using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDDHT.Models
{
    [Table("OrderCoupons", Schema = "shop")]
    public class OrderCoupon
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CouponId { get; set; }
        public decimal DiscountAmount { get; set; }

        // ⭐ ĐỔI CreatedAt THÀNH AppliedAt
        public DateTime? AppliedAt { get; set; }

        // Navigation Properties  
        public virtual Order Order { get; set; }
        public virtual Coupon Coupon { get; set; }

        public OrderCoupon()
        {
        }

        public OrderCoupon(int orderId, int couponId, decimal discountAmount)
        {
            OrderId = orderId;
            CouponId = couponId;
            DiscountAmount = discountAmount;
            AppliedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"OrderCoupon{{" +
                   $"Id={Id}, " +
                   $"OrderId={OrderId}, " +
                   $"CouponId={CouponId}, " +
                   $"DiscountAmount={DiscountAmount}, " +
                   $"AppliedAt={AppliedAt}" +
                   $"}}";
        }
    }
}