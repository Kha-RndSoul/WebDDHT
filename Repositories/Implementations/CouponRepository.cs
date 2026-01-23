using System;
using System.Collections.Generic;
using System.Linq;
using WebDDHT.Data;
using WebDDHT.Models;
using WebDDHT.Repositories.Interfaces;

namespace WebDDHT.Repositories.Implementations
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(SchoolSuppliesContext context) : base(context)
        {
        }

        public Coupon GetByCode(string couponCode)
        {
            return _dbSet.FirstOrDefault(c => c.CouponCode == couponCode);
        }

        public IEnumerable<Coupon> GetActiveCoupons()
        {
            var now = DateTime.Now;
            return _dbSet
                .Where(c => c.IsActive &&
                           (!c.StartDate.HasValue || c.StartDate <= now) &&
                           (!c.EndDate.HasValue || c.EndDate >= now))
                .OrderBy(c => c.CouponCode)
                .ToList();
        }

        public bool ValidateCoupon(string couponCode, decimal orderAmount, out string errorMessage)
        {
            errorMessage = string.Empty;

            var coupon = GetByCode(couponCode);

            if (coupon == null)
            {
                errorMessage = "Mã giảm giá không tồn tại.";
                return false;
            }

            if (!coupon.IsActive)
            {
                errorMessage = "Mã giảm giá đã bị vô hiệu hóa.";
                return false;
            }

            var now = DateTime.Now;

            if (coupon.StartDate.HasValue && coupon.StartDate > now)
            {
                errorMessage = "Mã giảm giá chưa bắt đầu có hiệu lực.";
                return false;
            }

            if (coupon.EndDate.HasValue && coupon.EndDate < now)
            {
                errorMessage = "Mã giảm giá đã hết hạn.";
                return false;
            }

            if (coupon.UsedCount >= coupon.MaxUses)
            {
                errorMessage = "Mã giảm giá đã hết lượt sử dụng.";
                return false;
            }

            if (coupon.MinOrderAmount.HasValue && orderAmount < coupon.MinOrderAmount.Value)
            {
                errorMessage = $"Đơn hàng tối thiểu {coupon.MinOrderAmount.Value:N0} VNĐ để sử dụng mã này.";
                return false;
            }

            return true;
        }

        public void IncrementUsedCount(int couponId)
        {
            var coupon = GetById(couponId);
            if (coupon != null)
            {
                coupon.UsedCount++;
                Update(coupon);
            }
        }
    }
}