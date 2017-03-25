using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models.Orders
{
	public class OrderCategory
	{
		private ICollection<Order> orders;

		public OrderCategory()
		{
			this.orders = new HashSet<Order>();
		}

		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Slug { get; set; }

		public string Description { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime LastModified { get; set; }

		public ICollection<Order> Orders
		{
			get { return orders; }
			set { orders = value; }
		}
	}
}