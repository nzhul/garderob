using App.Data.Service.Abstraction;
using App.Models;
using App.Models.Orders;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	[Authorize]
	public class OrdersController : Controller
	{
		private IOrdersService ordersService;
		private IMaterialsService materialsService;
		private IClientsService clientsService;

		public OrdersController(
			IOrdersService ordersService, 
			IMaterialsService materialsService,
			IClientsService clientsService)
		{
			this.ordersService = ordersService;
			this.materialsService = materialsService;
			this.clientsService = clientsService;
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
			// TODO: Do manual javascript validation for all hidden input fields and uploaded images
			if (ModelState.IsValid)
			{
				ApplicationUser currentUser = this.clientsService.GetUserById(this.User.Identity.GetUserId());
				model.ClientId = currentUser.Id;
				model.OrderCategoryId = this.ordersService.GetOrderCategoryBySlug("standard-wardrobes").Id;
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