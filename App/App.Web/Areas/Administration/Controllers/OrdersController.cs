using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Administration.Controllers
{
	public class OrdersController : BaseController
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}