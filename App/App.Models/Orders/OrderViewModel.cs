using System;

namespace App.Models.Orders
{
	public class OrderViewModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Slug { get; set; }

		public DateTime CompleteDate { get; set; }

		public OrderState State { get; set; }

		public decimal Price { get; set; }

		public int Count { get; set; }

		public byte[] SketchImage { get; set; }

		public byte[] DesignImage { get; set; }

		public byte[] ResultImage { get; set; }
	}
}