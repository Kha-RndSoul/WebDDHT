using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDDHT.Models
{
    [Table("OrderDetails", Schema = "shop")]
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }

        public decimal UnitPrice { get; set; }   
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }

        public DateTime? CreatedAt { get; set; }

        // Navigation Properties
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }

        public OrderDetail()
        {
        }
    }
}