using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebDDHT.Data;
using WebDDHT.Models;
using WebDDHT.Repositories.Interfaces;

namespace WebDDHT.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(SchoolSuppliesContext context) : base(context)
        {
        }

        public IEnumerable<Order> GetByCustomer(int customerId)
        {
            return _dbSet
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderDetails.Select(od => od.Product))
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
        }

        public Order GetByOrderCode(string orderCode)
        {
            return _dbSet
                .Include(o => o.OrderDetails.Select(od => od.Product))
                .Include(o => o.Customer)
                .FirstOrDefault(o => o.OrderCode == orderCode);
        }

        public Order GetOrderWithDetails(int orderId)
        {
            return _dbSet
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails.Select(od => od.Product.ProductImages))
                .Include(o => o.OrderCoupons.Select(oc => oc.Coupon))
                .Include(o => o.PaymentTransactions)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public IEnumerable<Order> GetByStatus(string status)
        {
            return _dbSet
                .Where(o => o.OrderStatus == status)
                .Include(o => o.Customer)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
        }

        public string GenerateOrderCode()
        {
            // Format: ORD20250122001
            string prefix = "ORD";
            string datePart = DateTime.Now.ToString("yyyyMMdd");

            // Get today's order count
            var today = DateTime.Now.Date;
            var todayOrderCount = _dbSet.Count(o => o.CreatedAt >= today);

            string sequencePart = (todayOrderCount + 1).ToString("D3");

            return $"{prefix}{datePart}{sequencePart}";
        }
    }
}