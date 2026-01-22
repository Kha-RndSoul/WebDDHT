using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDDHT.Models
{
    [Table("PaymentTransactions", Schema = "shop")]
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        [Required]
        [StringLength(20)]
        public string PaymentMethod { get; set; }

        [StringLength(20)]
        public string PaymentStatus { get; set; }

        public decimal Amount { get; set; }
        public string TransactionNote { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? CreatedAt { get; set; }

        // Navigation Property 
        public virtual Order Order { get; set; }

        public PaymentTransaction()
        {
        }
    }
}