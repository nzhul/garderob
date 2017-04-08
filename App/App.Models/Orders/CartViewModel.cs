using System.Collections.Generic;

namespace App.Models.Orders
{
	public class CartViewModel
	{
		public IEnumerable<OrderViewModel> Orders { get; set; }

		public IEnumerable<OrderViewModel> Cart { get; set; }
	}
}