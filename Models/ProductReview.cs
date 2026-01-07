using System;

namespace Shop.Models
{
    public class ProductReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public ProductReview()
        {
        }
        public ProductReview(int id, int productId, int customerId, int rating, string comment, bool status)
        {
            Id = id;
            ProductId = productId;
            CustomerId = customerId;
            Rating = rating;
            Comment = comment;
            Status = status;
        }
        public ProductReview(int id, int productId, int customerId, int rating,
                            string comment, bool status, DateTime createdAt)
        {
            Id = id;
            ProductId = productId;
            CustomerId = customerId;
            Rating = rating;
            Comment = comment;
            Status = status;
            CreatedAt = createdAt;
        }
        public override string ToString()
        {
            return $"ProductReview{{Id={Id}, ProductId={ProductId}, CustomerId={CustomerId}, " +
                   $"Rating={Rating}, Comment='{Comment}', Status={Status}, CreatedAt={CreatedAt}}}";
        }
    }
}