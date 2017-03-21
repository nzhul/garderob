using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class OrdersController : Controller
	{
		public ActionResult Make()
		{
			return this.View();
		}
	}
}