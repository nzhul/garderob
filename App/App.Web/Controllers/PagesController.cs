using App.Data.Service.Abstraction;
using App.Models.ViewModels;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class PagesController : Controller
	{
		private IPagesService pagesService;

		public PagesController(IPagesService pagesService)
		{
			this.pagesService = pagesService;
		}

		public ActionResult Index(string slug)
		{
			PageViewModel model = this.pagesService.GetPageBySlug(slug);

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