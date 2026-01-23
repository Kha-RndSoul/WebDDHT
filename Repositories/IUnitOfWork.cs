using System;
using WebDDHT.Repositories.Interfaces;

namespace WebDDHT.Repositories
{
    /// <summary>
    /// Unit of Work Pattern - Quản lý transactions và repositories
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        // ========== REPOSITORIES ==========

        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        ICartRepository Cart { get; }
        ICouponRepository Coupons { get; }

        // ========== TRANSACTION MANAGEMENT ==========

        /// <summary>
        /// Save tất cả changes vào database
        /// </summary>
        /// <returns>Số records affected</returns>
        int Complete();

        /// <summary>
        /// Bắt đầu transaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commit transaction (save changes + commit)
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback transaction
        /// </summary>
        void Rollback();
    }
}