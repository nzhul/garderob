using App.Data.Service.Abstraction;
using App.Models;
using App.Models.Orders;
using App.Models.Testimonials;
using System;
using System.Data.Entity;
using System.Linq;
using App.Models.InputModels;
using AutoMapper;

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

		public IQueryable<Testimonial> GetTestimonials(int? page, int? pagesize, bool approvedOnly)
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
				.Include(t => t.Client);

			if (approvedOnly)
			{
				testimonials = testimonials.Where(t => t.IsApproved == approvedOnly);
			}

			testimonials = testimonials.OrderByDescending(t => t.SubmissionDate).Skip(page.Value * pagesize.Value).Take(pagesize.Value);

			return testimonials;
		}

		public int GetTestimonialsCount()
		{
			return this.Data.Testimonials.All().Where(t => t.IsApproved == true).Count();
		}

		public Testimonial GetTestimonial(int id)
		{
			return this.Data.Testimonials.All()
				.Include(t => t.Order)
				.Include(t => t.Client)
				.Where(t => t.Id == id)
				.Single();
		}

		public Testimonial UpdateTestimonial(int id, EditTestimonialInputModel model)
		{
			Testimonial dbT = this.GetTestimonial(id);

			if (dbT != null)
			{
				dbT = Mapper.Map(model, dbT);
				this.Data.SaveChanges();
			}

			return dbT;
		}

		public Testimonial DeleteTestimonial(int id)
		{
			Testimonial testimonialToDelete = this.GetTestimonial(id);

			if (testimonialToDelete != null)
			{
				this.Data.Testimonials.Delete(testimonialToDelete);
				this.Data.SaveChanges();
			}

			return testimonialToDelete;
		}
	}
}