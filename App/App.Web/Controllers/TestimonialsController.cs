using App.Data.Service.Abstraction;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using App.Web.Areas.Administration.Models;
using System.Collections.Specialized;
using App.Models.Testimonials;
using System.Linq;
using AutoMapper;
using App.Models.Pages;

namespace App.Web.Controllers
{
	public class TestimonialsController : Controller
	{
		private ITestimonialsService testimonialsService;
		private const int defaultPageSize = 3;
		private const int defaultLinksRadius = 2;

		public TestimonialsController(ITestimonialsService testimonialsService)
		{
			this.testimonialsService = testimonialsService;
		}

		[HttpGet]
		public ActionResult List(int? page, int? pagesize)
		{
			IEnumerable<TestimonialViewModel> model = new List<TestimonialViewModel>();
			model = this.testimonialsService.GetTestimonials(page - 1, pagesize, true).ToList().Select(t => Mapper.Map(t, new TestimonialViewModel()));

			int totalTestimonialsCount = this.testimonialsService.GetTestimonialsCount();
			ViewBag.PagingData = this.GeneratePagingData(totalTestimonialsCount, pagesize ?? TestimonialsController.defaultPageSize, TestimonialsController.defaultLinksRadius);
			return View(model);
		}

		private PagingData GeneratePagingData(int totalItemsCount, int pageSize, int linksRadius)
		{
			string rawUrl = this.HttpContext.Request.Url.ToString();
			Uri pageUrl = new Uri(rawUrl);
			NameValueCollection queryString = this.HttpContext.Request.QueryString;

			return new PagingData(totalItemsCount, pageSize, linksRadius, false, pageUrl, queryString);
		}
	}
}