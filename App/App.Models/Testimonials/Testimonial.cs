using App.Models.Orders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Testimonials
{
	public class Testimonial
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Order")]
		public int OrderId { get; set; }

		public Order Order { get; set; }

		[ForeignKey("Client")]
		public string ClientId { get; set; }

		public ApplicationUser Client { get; set; }

		[Range(0, 5)]
		public double Rating { get; set; }

		public string Text { get; set; }

		public DateTime SubmissionDate { get; set; }

		public bool IsApproved { get; set; }
	}
}