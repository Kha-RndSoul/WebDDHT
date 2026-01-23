using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.Repositories.Interfaces
{
    public interface ICouponRepository : IGenericRepository<Coupon>
    {
        /// <summary>
        /// Lấy coupon theo code
        /// </summary>
        Coupon GetByCode(string couponCode);

        /// <summary>
        /// Lấy active coupons
        /// </summary>
        IEnumerable<Coupon> GetActiveCoupons();

        /// <summary>
        /// Validate coupon (check active, valid date, usage limit)
        /// </summary>
        bool ValidateCoupon(string couponCode, decimal orderAmount, out string errorMessage);

        /// <summary>
        /// Tăng UsedCount của coupon
        /// </summary>
        void IncrementUsedCount(int couponId);
    }
}