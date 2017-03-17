using App.Data;
using App.Data.Service;
using App.Models.ViewModels;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class PagesController : BaseController
	{
		private readonly IUoWData data;
		private readonly IPagesService pagesService;
		public PagesController()
		{
			this.data = new UoWData();
			this.pagesService = new PagesService(this.data);
		}

		public ActionResult Index(string urlName)
		{
			PageViewModel model = this.pagesService.GetPageByUrlName(urlName);
			ViewBag.Title = model.Title;
			ViewBag.MetaDescription = model.Summary;
			return View(model);
		}
	}
}