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