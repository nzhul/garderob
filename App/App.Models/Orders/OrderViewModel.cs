using App.Models.Images;
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

		public bool IsInCart { get; set; }

		public Image SketchImage { get; set; }

		public Image DesignImage { get; set; }

		public Image ResultImage { get; set; }

		public bool Installation { get; set; }
	}
}