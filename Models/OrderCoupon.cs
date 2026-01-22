using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebDDHT.Models
{
    public class OrderCoupon
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CouponId { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime? CreatedAt { get; set; }

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
        }

        public override string ToString()
        {
            return $"OrderCoupon{{" +
                   $"Id={Id}, " +
                   $"OrderId={OrderId}, " +
                   $"CouponId={CouponId}, " +
                   $"DiscountAmount={DiscountAmount}, " +
                   $"CreatedAt={CreatedAt}" +
                   $"}}";
        }
    }
}