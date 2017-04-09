namespace App.Models.Orders
{
	public class ProductViewModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Slug { get; set; }

		public decimal Price { get; set; }

		public byte[] ResultImageSmall { get; set; }

		public byte[] ResultImageBig { get; set; }
	}
}