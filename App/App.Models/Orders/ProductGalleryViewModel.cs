using System.Collections.Generic;

namespace App.Models.Orders
{
	public class ProductGalleryViewModel
	{
		public IEnumerable<CategoryItem> Categories { get; set; }

		public IEnumerable<ProductViewModel> Products { get; set; }
	}

	public class CategoryItem
	{
		public string Title { get; set; }

		public string Slug { get; set; }
	}
}