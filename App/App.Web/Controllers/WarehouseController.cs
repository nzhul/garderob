using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	[Authorize]
	public class WarehouseController : Controller
	{
		public ActionResult Orders()
		{
			return this.View();
		}
	}
}