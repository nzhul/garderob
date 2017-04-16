using App.Models.Testimonials;
using System.Linq;
using App.Models.InputModels;

namespace App.Data.Service.Abstraction
{
	public interface ITestimonialsService
	{
		Testimonial AddTestimonial(TestimonialInputModel model, string userId);

		IQueryable<Testimonial> GetTestimonials(int? v, int? pagesize, bool approvedOnly);

		int GetTestimonialsCount();

		Testimonial GetTestimonial(int id);

		Testimonial UpdateTestimonial(int id, EditTestimonialInputModel model);

		Testimonial DeleteTestimonial(int id);
	}
}
