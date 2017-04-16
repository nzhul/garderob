using App.Data.Service.Abstraction;
using App.Models.InputModels;
using App.Models.Pages;
using App.Models.Testimonials;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Areas.Administration.Controllers
{
	public class TestimonialsController : BaseController
	{
		ITestimonialsService testimonialsService;
		private const int defaultPageSize = 10;
		private const int defaultLinksRadius = 2;

		public TestimonialsController(ITestimonialsService testimonialsService)
		{
			this.testimonialsService = testimonialsService;
		}

		[HttpGet]
		public ActionResult Index(int? page, int? pagesize)
		{
			if (page == null || page < 0)
			{
				page = 1;
			}

			if (pagesize == null || pagesize < 0)
			{
				pagesize = TestimonialsController.defaultPageSize;
			}

			IEnumerable<TestimonialSimpleViewModel> model = 
					this.testimonialsService.GetTestimonials(page - 1, pagesize, false)
						.ToList().Select(t => Mapper.Map(t, new TestimonialSimpleViewModel()));

			int totalCount = this.testimonialsService.GetTestimonialsCount();
			ViewBag.PagingData = this.GeneratePaginationData(totalCount, pagesize ?? TestimonialsController.defaultPageSize, TestimonialsController.defaultLinksRadius);
			return View(model);
		}

		//TODO: extract this into utility class. It is used on atleast 3 places
		private PagingData GeneratePaginationData(int totalItemsCount, int pageSize, int linksRadius)
		{
			string rawUrl = this.HttpContext.Request.Url.ToString();
			Uri pageUrl = new Uri(rawUrl);
			NameValueCollection queryString = this.HttpContext.Request.QueryString;

			return new PagingData(totalItemsCount, pageSize, linksRadius, false, pageUrl, queryString);
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{

			Testimonial dbT = this.testimonialsService.GetTestimonial(id);
			if (dbT != null)
			{
				EditTestimonialInputModel model = Mapper.Map(dbT, new EditTestimonialInputModel());
				return this.View(model);
			}

			return HttpNotFound();
		}

		[HttpPost]
		public ActionResult Edit(int id, EditTestimonialInputModel model)
		{
			if (ModelState.IsValid)
			{
				Testimonial updatedTestimonial = this.testimonialsService.UpdateTestimonial(id, model);
				if (updatedTestimonial != null)
				{
					TempData["message"] = "Атестата беше редактиран успешно!";
					TempData["messageType"] = "success";
					return RedirectToAction("Index");
				}
			}

			TempData["message"] = "Невалидни данни!<br/> Моля попълнете <strong>всички</strong> задължителни полета!";
			TempData["messageType"] = "danger";
			return View(model);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			Testimonial deletedTestimonial = this.testimonialsService.DeleteTestimonial(id);

			if (deletedTestimonial != null)
			{
				TempData["message"] = "Изтрито успешно!";
				TempData["messageType"] = "success";
				return this.RedirectToAction("Index");
			}

			return HttpNotFound();
		}
	}
}