using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebDDHT.Data;
using WebDDHT.Models;
using WebDDHT.Repositories.Interfaces;

namespace WebDDHT.Repositories.Implementations
{
    public class CartRepository : GenericRepository<CartItem>, ICartRepository
    {
        public CartRepository(SchoolSuppliesContext context) : base(context)
        {
        }

        public IEnumerable<CartItem> GetByCustomer(int customerId)
        {
            return _dbSet
                .Where(c => c.CustomerId == customerId)
                .Include(c => c.Product.ProductImages)
                .ToList();
        }

        public CartItem GetCartItem(int customerId, int productId)
        {
            return _dbSet.FirstOrDefault(c => c.CustomerId == customerId && c.ProductId == productId);
        }

        public void ClearCart(int customerId)
        {
            var cartItems = _dbSet.Where(c => c.CustomerId == customerId);
            _dbSet.RemoveRange(cartItems);
        }

        public int CountItems(int customerId)
        {
            return _dbSet.Count(c => c.CustomerId == customerId);
        }

        public decimal GetCartTotal(int customerId)
        {
            return _dbSet
                .Where(c => c.CustomerId == customerId)
                .Include(c => c.Product)
                .AsEnumerable() // Switch to client-side evaluation
                .Sum(c => c.Product.GetCurrentPrice() * c.Quantity);
        }
    }
}