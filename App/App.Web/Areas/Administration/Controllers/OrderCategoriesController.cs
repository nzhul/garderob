using App.Data.Service.Abstraction;
using App.Models.InputModels;
using App.Models.Orders;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Administration.Controllers
{
	public class OrderCategoriesController : BaseController
	{
		IOrdersService ordersService;

		public OrderCategoriesController(IOrdersService ordersService)
		{
			this.ordersService = ordersService;
		}

		[HttpGet]
		public ActionResult Index()
		{
			IEnumerable<OrderCategoryViewModel> model = this.ordersService.GetOrderCategories().ToList().Select(c => Mapper.Map(c, new OrderCategoryViewModel()));
			return View(model);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(EditOrderCategoryInputModel categoryInput)
		{
			if (ModelState.IsValid)
			{
				int result = this.ordersService.CreateOrderCategory(categoryInput);
				if (result > 0)
				{
					TempData["message"] = "Успешно добавихте нова категория!";
					TempData["messageType"] = "success";
					return RedirectToAction("Index");
				}
			}

			TempData["message"] = "Невалидни данни за категорията!<br/> Моля попълнете <strong>всички</strong> задължителни полета!";
			TempData["messageType"] = "danger";
			return View(categoryInput);
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			OrderCategory dbCategory =  this.ordersService.GetOrderCategoryById(id);
			EditOrderCategoryInputModel model = Mapper.Map(dbCategory, new EditOrderCategoryInputModel());
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(int id, EditOrderCategoryInputModel model)
		{
			if (ModelState.IsValid)
			{
				OrderCategory updatedCategory = this.ordersService.UpdateOrderCategory(id, model);

				if (updatedCategory != null)
				{
					TempData["message"] = "Редактирахте успешно категорията!";
					TempData["messageType"] = "success";
					return RedirectToAction("Index");
				}
				else
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}

			TempData["message"] = "Невалидни данни за категорията!<br/> Моля попълнете <strong>всички</strong> полета в червено!";
			TempData["messageType"] = "danger";
			return View(model);
		}
	}
}