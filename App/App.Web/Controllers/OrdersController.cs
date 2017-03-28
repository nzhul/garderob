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
		public ActionResult Make()
		{
			return this.View();
		}

		[HttpPost]
		public ActionResult Make(OrderInputModel model)
		{
			return this.View(model);
		}
	}
}