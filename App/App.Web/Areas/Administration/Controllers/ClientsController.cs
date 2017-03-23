using App.Data;
using App.Data.Service;
using System.Web.Mvc;
using System.Linq;
using PagedList;

namespace App.Web.Areas.Administration.Controllers
{
	public class ClientsController : Controller
	{
		private readonly IClientsServices clientsService;
		private readonly IUoWData data;

		public ClientsController()
		{
			this.data = new UoWData();
			this.clientsService = new ClientsService(data);
		}

		public ActionResult Index(int? page)
		{
			IPagedList pagedList = this.clientsService.GetUsers().ToList().ToPagedList(page ?? 1, 3);
			return View(pagedList);
		}
	}
}