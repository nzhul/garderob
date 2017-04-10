using App.Data.Service.Abstraction;
using App.Models;
using App.Models.InputModels;
using App.Models.Pages;
using App.Web.Areas.Administration.Models;
using AutoMapper;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Areas.Administration.Controllers
{
	public class ClientsController : BaseController
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
			ClientsMasterModel model = new ClientsMasterModel();
			model.ActiveUsers = this.clientsService.GetUsers(page - 1, pagesize).ToList();
			model.InActiveUsers = this.clientsService.GetInactiveUsers().ToList();

			int totalClientsCount = this.clientsService.GetUsersCount();
			ViewBag.PagingData = this.GeneratePagingData(totalClientsCount, pagesize ?? ClientsController.defaultPageSize, ClientsController.defaultLinksRadius);
			return View(model);
		}

		private PagingData GeneratePagingData(int totalItemsCount, int pageSize, int linksRadius)
		{
			string rawUrl = this.HttpContext.Request.Url.ToString();
			Uri pageUrl = new Uri(rawUrl);
			NameValueCollection queryString = this.HttpContext.Request.QueryString;

			return new PagingData(totalItemsCount, pageSize, linksRadius, false, pageUrl, queryString);
		}

		[HttpGet]
		public ActionResult Edit(string id)
		{
			EditClientInputModel model = new EditClientInputModel();

			if (this.clientsService.ClientExists(id))
			{
				ApplicationUser dbUser = this.clientsService.GetUserById(id);
				model = Mapper.Map(dbUser, model);
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(string id, EditClientInputModel inputModel)
		{
			if (ModelState.IsValid)
			{
				bool IsUpdateSuccessfull = this.clientsService.UpdateClient(id, inputModel);
				if (IsUpdateSuccessfull)
				{
					TempData["message"] = "Клиента беше редактиран успешно!";
					TempData["messageType"] = "success";
					return RedirectToAction("Index");
				}
			}
			TempData["message"] = "Невалидни данни за клиента!<br/> Моля попълнете <strong>всички</strong> задължителни полета!";
			TempData["messageType"] = "danger";
			return View(inputModel);
		}

		[HttpGet]
		public ActionResult DeactivateClient(string id)
		{
			this.clientsService.DeactivateClient(id);

			return this.RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult ActivateClient(string id)
		{
			this.clientsService.ActivateClient(id);

			return this.RedirectToAction("Index");
		}
	}
}