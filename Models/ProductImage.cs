using System;

namespace Shop.Models
{
	public class ProductImage
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string ImageUrl { get; set; }
		public bool IsPrimary { get; set; }
		public DateTime CreatedAt { get; set; }

		public ProductImage()
		{
		}
		public ProductImage(int id, int productId, string imageUrl, bool isPrimary)
		{
			Id = id;
			ProductId = productId;
			ImageUrl = imageUrl;
			IsPrimary = isPrimary;
		}
		public ProductImage(int id, int productId, string imageUrl, bool isPrimary, DateTime createdAt)
		{
			Id = id;
			ProductId = productId;
			ImageUrl = imageUrl;
			IsPrimary = isPrimary;
			CreatedAt = createdAt;
		}
		public override string ToString()
		{
			return $"ProductImage{{Id={Id}, ProductId={ProductId}, ImageUrl='{ImageUrl}', " +
				   $"IsPrimary={IsPrimary}, CreatedAt={CreatedAt}}}";
		}
	}
}