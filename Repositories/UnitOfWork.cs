using System;
using System.Data.Entity;
using WebDDHT.Data;
using WebDDHT.Repositories.Implementations;
using WebDDHT.Repositories.Interfaces;

namespace WebDDHT.Repositories
{
    /// <summary>
    /// Unit of Work Implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolSuppliesContext _context;
        private DbContextTransaction _transaction;

        // Repositories - Lazy initialization
        private ICategoryRepository _categories;
        private IProductRepository _products;
        private ICustomerRepository _customers;
        private IOrderRepository _orders;
        private ICartRepository _cart;
        private ICouponRepository _coupons;

        public UnitOfWork(SchoolSuppliesContext context)
        {
            _context = context;
        }

        // ========== REPOSITORY PROPERTIES ==========

        public ICategoryRepository Categories
        {
            get { return _categories ?? (_categories = new CategoryRepository(_context)); }
        }

        public IProductRepository Products
        {
            get { return _products ?? (_products = new ProductRepository(_context)); }
        }

        public ICustomerRepository Customers
        {
            get { return _customers ?? (_customers = new CustomerRepository(_context)); }
        }

        public IOrderRepository Orders
        {
            get { return _orders ?? (_orders = new OrderRepository(_context)); }
        }

        public ICartRepository Cart
        {
            get { return _cart ?? (_cart = new CartRepository(_context)); }
        }

        public ICouponRepository Coupons
        {
            get { return _coupons ?? (_coupons = new CouponRepository(_context)); }
        }

        // ========== TRANSACTION MANAGEMENT ==========

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                Complete();
                _transaction?.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        // ========== DISPOSE ==========

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}