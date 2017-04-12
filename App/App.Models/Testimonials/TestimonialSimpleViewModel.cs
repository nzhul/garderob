using System;

namespace App.Models.Testimonials
{
	public class TestimonialSimpleViewModel
	{
		public int Id { get; set; }

		public string ClientId { get; set; }

		public string ClientFullName { get; set; }

		public string OrderTitle { get; set; }

		public string OrderId { get; set; }

		public double Rating { get; set; }

		public DateTime SubmissionDate { get; set; }
	}
}