using App.Models.Testimonials;
using System.Collections.Generic;

namespace App.Web.Models
{
	public class IndexViewModel
	{
		public IEnumerable<TestimonialViewModel> Testimonials { get; set; }
	}
}