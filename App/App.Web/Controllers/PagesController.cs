using App.Data;
using App.Data.Service;
using App.Models.ViewModels;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class PagesController : BaseController
	{
		public ActionResult Index(string urlName)
		{
			PageViewModel model = this.pagesService.GetPageByUrlName(urlName);

			if (model != null)
			{
				ViewBag.Title = model.Title;
				ViewBag.MetaDescription = model.Summary;
				return View(model);
			}
			else
			{
				return HttpNotFound();
			}
		}
	}
}