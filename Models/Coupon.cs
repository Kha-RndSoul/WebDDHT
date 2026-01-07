using System;

namespace Shop.Model
{
	public class Coupon
	{
		public int CouponId { get; set; }
		public string CouponCode { get; set; }
		public string DiscountType { get; set; }
		public decimal DiscountValue { get; set; }
		public decimal? MinOrderAmount { get; set; }
		public int MaxUses { get; set; }
		public int UsedCount { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool IsActive { get; set; }
		public DateTime? CreatedAt { get; set; }


		public Coupon()
		{
		}

		public Coupon(
			int couponId,
			string couponCode,
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
			DiscountType = discountType;
			DiscountValue = discountValue;
			MinOrderAmount = minOrderAmount;
			MaxUses = maxUses;
			UsedCount = usedCount;
			StartDate = startDate;
			EndDate = endDate;
			IsActive = isActive;
			CreatedAt = createdAt;
		}

		public override string ToString()
		{
			return $"Coupon{{" +
				   $"CouponId={CouponId}, " +
				   $"CouponCode='{CouponCode}', " +
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
