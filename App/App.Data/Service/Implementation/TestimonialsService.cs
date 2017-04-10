using App.Data.Service.Abstraction;
using App.Models;
using App.Models.Orders;
using App.Models.Testimonials;
using System;
using System.Data.Entity;
using System.Linq;

namespace App.Data.Service.Implementation
{
	public class TestimonialsService : ITestimonialsService
	{
		private IUoWData Data;
		private const int defaultPageSize = 3;
		private const int defaultPage = 0;

		public TestimonialsService(IUoWData data)
		{
			this.Data = data;
		}

		public Testimonial AddTestimonial(TestimonialInputModel model, string userId)
		{
			Order dbOrder = this.Data.Orders.Find(model.OrderId);
			ApplicationUser dbUser = this.Data.Users.Find(userId);

			if (dbOrder != null && dbUser != null && dbOrder.Client.Id == dbUser.Id)
			{
				Testimonial newTestimonial = new Testimonial
				{
					Client = dbUser,
					Order = dbOrder,
					Rating = model.Rating,
					SubmissionDate = DateTime.UtcNow,
					Text = model.Text
				};

				dbOrder.Testimonials.Add(newTestimonial);
				this.Data.SaveChanges();

				return newTestimonial;
			}
			else
			{
				return null;
			}
		}

		public IQueryable<Testimonial> GetTestimonials(int? page, int? pagesize)
		{
			if (page == null || page < 0)
			{
				page = defaultPage;
			}

			if (pagesize == null || pagesize < 1)
			{
				pagesize = defaultPageSize;
			}

			IQueryable<Testimonial> testimonials = this.Data.Testimonials
				.All()
				.Include(t => t.Order)
				.Include(t => t.Client)
				.Where(t => t.IsApproved == true)
				.OrderByDescending(t=>t.SubmissionDate);

			testimonials = testimonials.Skip(page.Value * pagesize.Value).Take(pagesize.Value);

			return testimonials;
		}

		public int GetTestimonialsCount()
		{
			return this.Data.Testimonials.All().Where(t => t.IsApproved == true).Count();
		}
	}
}