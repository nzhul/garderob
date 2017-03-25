using App.Data.Service;
using App.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Areas.Administration.Controllers
{
	public class ClientsController : Controller
	{
		private IClientsService clientsService;

		public ClientsController(IClientsService clientsService)
		{
			this.clientsService = clientsService;
		}

		public ActionResult Index(int? page)
		{
			ICollection<ApplicationUser> clients = this.clientsService.GetUsers().ToList();
			return View(clients);
		}
	}
}