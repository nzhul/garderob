using App.Models.Testimonials;
using System.Linq;

namespace App.Data.Service.Abstraction
{
	public interface ITestimonialsService
	{
		Testimonial AddTestimonial(TestimonialInputModel model, string userId);
		IQueryable<Testimonial> GetTestimonials(int? v, int? pagesize);
		int GetTestimonialsCount();
	}
}
