using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDDHT.Models
{
    [Table("Orders", Schema = "shop")]
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderCode { get; set; }

        [StringLength(20)]
        public string OrderStatus { get; set; }

        [StringLength(20)]
        public string PaymentMethod { get; set; }

        [StringLength(20)]
        public string PaymentStatus { get; set; }

        public decimal TotalAmount { get; set; }

        [StringLength(100)]
        public string ShippingName { get; set; }

        [StringLength(20)]
        public string ShippingPhone { get; set; }

        [StringLength(500)]
        public string ShippingAddress { get; set; }

        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<OrderCoupon> OrderCoupons { get; set; }
        public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            OrderCoupons = new List<OrderCoupon>();
            PaymentTransactions = new List<PaymentTransaction>();
        }
    }
}