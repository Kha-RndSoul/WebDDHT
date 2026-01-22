using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebDDHT.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }

        [Required]
        [StringLength(50)]
        public string CouponCode { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }       

        [Required]
        [StringLength(20)]
        public string DiscountType { get; set; }

        public decimal DiscountValue { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public int MaxUses { get; set; }
        public int UsedCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Navigation Properties
        public virtual ICollection<OrderCoupon> OrderCoupons { get; set; }  

        public Coupon()
        {
            OrderCoupons = new List<OrderCoupon>();  
        }

        public Coupon(
            int couponId,
            string couponCode,
            string imageUrl,               
            string discountType,
            decimal discountValue,
            decimal? minOrderAmount,
            int maxUses,
            int usedCount,
            DateTime? startDate,
            DateTime? endDate,
            bool isActive,
            DateTime? createdAt
        )
        {
            CouponId = couponId;
            CouponCode = couponCode;
            ImageUrl = imageUrl;           
            DiscountType = discountType;
            DiscountValue = discountValue;
            MinOrderAmount = minOrderAmount;
            MaxUses = maxUses;
            UsedCount = usedCount;
            StartDate = startDate;
            EndDate = endDate;
            IsActive = isActive;
            CreatedAt = createdAt;
            OrderCoupons = new List<OrderCoupon>();  
        }

        public override string ToString()
        {
            return $"Coupon{{" +
                   $"CouponId={CouponId}, " +
                   $"CouponCode='{CouponCode}', " +
                   $"ImageUrl='{ImageUrl}', " +     
                   $"DiscountType='{DiscountType}', " +
                   $"DiscountValue={DiscountValue}, " +
                   $"MinOrderAmount={MinOrderAmount}, " +
                   $"MaxUses={MaxUses}, " +
                   $"UsedCount={UsedCount}, " +
                   $"StartDate={StartDate}, " +
                   $"EndDate={EndDate}, " +
                   $"IsActive={IsActive}, " +
                   $"CreatedAt={CreatedAt}" +
                   $"}}";
        }
    }
}