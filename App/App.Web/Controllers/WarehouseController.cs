﻿using App.Data.Service.Abstraction;
using App.Models.Orders;
using App.Models.Testimonials;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	[Authorize]
	public class WarehouseController : Controller
	{
		private IOrdersService ordersService;
		private ITestimonialsService testimonialsService;

		public WarehouseController(IOrdersService ordersService, ITestimonialsService testimonialsService)
		{
			this.ordersService = ordersService;
			this.testimonialsService = testimonialsService;
		}

		public ActionResult Orders()
		{
			IList<Order> dbOrders = this.ordersService.GetUserOrders(this.User.Identity.GetUserId()).ToList();
			IEnumerable<OrderViewModel> orders = dbOrders.Select(o => AutoMapper.Mapper.Map(o, new OrderViewModel()));

			IList<Order> dbCart = this.ordersService.GetUserCart(this.User.Identity.GetUserId()).ToList();
			IEnumerable<OrderViewModel> cart = dbCart.Select(o => AutoMapper.Mapper.Map(o, new OrderViewModel()));

			CartViewModel model = new CartViewModel
			{
				Orders = orders,
				Cart = cart
			};

			return this.View(model);
		}

		[HttpPost]
		public ActionResult AddCartItem(int orderId, int orderCount, bool installation)
		{
			string userId = this.User.Identity.GetUserId();
			Order dbOrder = this.ordersService.AddCartItem(orderId, orderCount, installation, userId);
			if (dbOrder != null)
			{
				return Json(
					new
					{
						Status = "Success",
						Data = new
						{
							id = dbOrder.Id,
							count = dbOrder.Count,
							title = dbOrder.Title,
							installation = dbOrder.Installation,
							price = dbOrder.Price,
						}
					});
			}
			else
			{
				return Json(new { Status = "Fail" });
			}
		}

		[HttpPost]
		public ActionResult RemoveCartItem(int orderId)
		{
			string userId = this.User.Identity.GetUserId();
			if (this.ordersService.RemoveCartItem(orderId, userId))
			{
				return Json(new { Status = "Success" });
			}
			else
			{
				// order or client was not found, or the order do not belong to the user ( hack )
				return Json(new { Status = "Fail" });
			}
		}

		[HttpPost]
		public ActionResult OrderNow(string paymentType)
		{
			string userId = this.User.Identity.GetUserId();
			if (this.ordersService.OrderNow(userId, paymentType))
			{
				return Json(new { Status = "Success" });
			}
			else
			{
				return Json(new { Status = "Fail" });
			}
		}

		[HttpGet]
		public ActionResult AddTestimonial(int id)
		{
			TestimonialInputModel model = new TestimonialInputModel();

			Order dbOrder = this.ordersService.GetOrder(id);
			if (dbOrder != null)
			{
				string orderTitle = this.ordersService.GetOrder(id).Title;
				model.OrderTitle = orderTitle;
				model.OrderId = id;
				return this.View(model);
			}

			return HttpNotFound();
		}

		[HttpPost]
		public ActionResult AddTestimonial(int id, TestimonialInputModel model)
		{
			if (ModelState.IsValid)
			{
				string userId = this.User.Identity.GetUserId();
				Testimonial dbTestimonial = this.testimonialsService.AddTestimonial(model, userId);

				if (dbTestimonial != null)
				{
					return this.RedirectToAction("SuccessTestimonial");
				}
				else
				{
					ModelState.AddModelError("InvalidOrderId", " * Имате право да изпращате атестати само за поръчки които са ваши!");
					return View(model);
				}
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult SuccessTestimonial()
		{
			return this.View();
		}
	}
}