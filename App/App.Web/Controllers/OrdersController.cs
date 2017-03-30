using App.Data.Service;
using App.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		private IOrdersService ordersService;

		public OrdersController(IOrdersService ordersService)
		{
			this.ordersService = ordersService;
		}

		public ActionResult Make()
		{
			return this.View();
		}

		[HttpPost]
		public ActionResult Make(OrderInputModel model)
		{
			if (ModelState.IsValid)
			{
				int newPageId = this.ordersService.MakeOrder(model);
				if (newPageId > 0)
				{
					TempData["message"] = "Заявката беше направена успешно!";
					TempData["messageType"] = "success";
					return View(model);
				}
			}

			TempData["message"] = "Невалидни данни!<br/> Моля попълнете <strong>всички</strong> задължителни полета!";
			TempData["messageType"] = "danger";
			return View(model);
		}
	}
}