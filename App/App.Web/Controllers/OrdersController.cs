using App.Data.Service.Abstraction;
using App.Models;
using App.Models.Orders;
using Microsoft.AspNet.Identity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Controllers
{
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
			if (Request.IsAuthenticated)
			{
				ApplicationUser currentUser = this.clientsService.GetUserById(this.User.Identity.GetUserId());
				model.ClientId = currentUser.Id;
				model.AnonymousClientEmail = currentUser.Email;
				model.AnonymousClientName = currentUser.FirstName + " " + currentUser.LastName;
				model.AnonymousClientPhone = currentUser.PhoneNumber;

				ModelState.SetModelValue("AnonymousClientName", new ValueProviderResult(currentUser.FirstName + " " + currentUser.LastName, "", CultureInfo.InvariantCulture));
				ModelState.SetModelValue("AnonymousClientPhone", new ValueProviderResult(currentUser.PhoneNumber, "", CultureInfo.InvariantCulture));
				ModelState.SetModelValue("AnonymousClientEmail", new ValueProviderResult(currentUser.Email, "", CultureInfo.InvariantCulture));

				ModelState["AnonymousClientEmail"].Errors.Clear();
				ModelState["AnonymousClientName"].Errors.Clear();
				ModelState["AnonymousClientPhone"].Errors.Clear();
			}

			if (TryValidateModel(model))
			{
				if (ModelState.IsValid)
				{
					model.OrderCategoryId = this.ordersService.GetOrderCategoryBySlug("standard-wardrobes").Id;
					int orderId = this.ordersService.MakeOrder(model);
					if (orderId > 0)
					{
						return this.RedirectToAction("Success");
					}
				}
			}

			model.SurfaceMaterials = this.materialsService.GetAllMaterials("surfaces").ToList();
			model.HandlesMaterials = this.materialsService.GetAllMaterials("handles").ToList();

			return View(model);
		}

		public ActionResult Success()
		{
			return this.View();
		}

		[HttpGet]
		public ActionResult Copy(int id)
		{
			if (this.Request.IsAuthenticated)
			{
				ApplicationUser currentUser = this.clientsService.GetUserById(this.User.Identity.GetUserId());
				int newOrderId = this.ordersService.CopyOrder(currentUser, id);
				if (newOrderId > 0)
				{
					return this.RedirectToAction("Orders", "Warehouse");
				}
			}

			return this.HttpNotFound();
		}
	}
}