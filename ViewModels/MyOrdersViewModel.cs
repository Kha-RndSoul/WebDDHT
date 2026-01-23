using System.Collections.Generic;
using WebDDHT.Models;

namespace WebDDHT.ViewModels
{
    /// <summary>
    /// ViewModel cho trang đơn hàng của tôi
    /// </summary>
    public class MyOrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public Customer Customer { get; set; }

        public MyOrdersViewModel()
        {
            Orders = new List<Order>();
        }
    }
}