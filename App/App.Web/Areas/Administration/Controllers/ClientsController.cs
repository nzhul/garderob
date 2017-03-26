using App.Data.Service;
using App.Models;
using App.Web.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Areas.Administration.Controllers
{
	public class ClientsController : Controller
	{
		private IClientsService clientsService;
		private const int defaultPageSize = 10;
		private const int defaultLinksRadius = 2;

		public ClientsController(IClientsService clientsService)
		{
			this.clientsService = clientsService;
		}

		[HttpGet]
		public ActionResult Index(int? page, int? pagesize)
		{
			ICollection<ApplicationUser> clients = this.clientsService.GetUsers(page - 1, pagesize).ToList();

			int totalClientsCount = this.clientsService.GetUsersCount();
			ViewBag.PagingData = this.GeneratePagingData(totalClientsCount, pagesize ?? ClientsController.defaultPageSize, ClientsController.defaultLinksRadius);
			return View(clients);
		}

		private PagingData GeneratePagingData(int totalItemsCount, int pageSize, int linksRadius)
		{
			string rawUrl = this.HttpContext.Request.Url.ToString();
			Uri pageUrl = new Uri(rawUrl);
			NameValueCollection queryString = this.HttpContext.Request.QueryString;

			return new PagingData(totalItemsCount, pageSize, linksRadius, false, pageUrl, queryString);
		}
	}
}