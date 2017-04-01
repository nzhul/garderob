using App.Data.Service.Abstraction;
using App.Models.Orders;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		private IOrdersService ordersService;
		private IMaterialsService materialsService;

		public OrdersController(IOrdersService ordersService, IMaterialsService materialsService)
		{
			this.ordersService = ordersService;
			this.materialsService = materialsService;
		}

		public ActionResult Make()
		{
			OrderInputModel model = new OrderInputModel
			{
				// TODO: do view model in order to skip big images!
				SurfaceMaterials = this.materialsService.GetAllMaterials("surfaces").ToList(),
				HandlesMaterials = this.materialsService.GetAllMaterials("handles").ToList()
			};

			return this.View(model);
		}

		[HttpPost]
		public ActionResult Make(OrderInputModel model)
		{
			if (ModelState.IsValid)
			{
				int newPageId = this.ordersService.MakeOrder(model);
				if (newPageId > 0)
				{
					TempData["message"] = "Заявката беше направена успешно!";
					TempData["messageType"] = "success";
					return View(model);
				}
			}

			TempData["message"] = "Невалидни данни!<br/> Моля попълнете <strong>всички</strong> задължителни полета!";
			TempData["messageType"] = "danger";
			return View(model);
		}
	}
}