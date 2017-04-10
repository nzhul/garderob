using App.Models.Images;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models.Testimonials
{
	public class TestimonialViewModel
	{
		public Image OrderSketch { get; set; }

		public Image OrderDesign { get; set; }

		public Image OrderResult { get; set; }

		public byte[] ClientPhoto { get; set; }

		public string ClientFullName { get; set; }

		public string ClientJobTitle { get; set; }

		[Range(0, 5)]
		public double Rating { get; set; }

		public string Text { get; set; }

		public DateTime SubmissionDate { get; set; }
	}
}
