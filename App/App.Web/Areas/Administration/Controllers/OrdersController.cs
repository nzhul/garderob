using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Areas.Administration.Controllers
{
	public class OrdersController : BaseController
	{
		public ActionResult Index()
		{
			string sender = ConfigurationManager.AppSettings["emailSender"];
			string receiver = ConfigurationManager.AppSettings["emailReceiver"];
			return View();
		}
	}
}