using App.Data;
using App.Data.Service;
using App.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class BaseController : Controller
	{
		protected IUoWData data;
		protected IPagesService pagesService;
		protected IClientsServices clientsService;

		public BaseController()
		{
			this.data = new UoWData();
			this.pagesService = new PagesService(this.data);
			this.clientsService = new ClientsService(this.data);

			LayoutModel model = new LayoutModel();
			model.Pages = this.pagesService.GetPages();

			ViewBag.LayoutModel = model;
		}
	}
}