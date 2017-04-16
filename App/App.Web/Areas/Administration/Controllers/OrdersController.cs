using App.Data.Service.Abstraction;
using App.Models.InputModels;
using App.Models.Messages;
using App.Models.Orders;
using App.Web.Areas.Administration.Models;
using AutoMapper;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Areas.Administration.Controllers
{
	public class OrdersController : BaseController
	{
		IOrdersService ordersService;
		IMaterialsService materialsService;
		IImagesService imagesService;

		public OrdersController(IOrdersService ordersService, IMaterialsService materialsService, IImagesService imagesService)
		{
			this.ordersService = ordersService;
			this.materialsService = materialsService;
			this.imagesService = imagesService;
		}

		[HttpGet]
		public ActionResult Index()
		{
			OrdersMasterModel model = new OrdersMasterModel();
			model.InProduction = this.ordersService.GetOrdersByState(OrderState.InProduction).ToList().Select(o => Mapper.Map(o, new OrderViewModelSimple()));
			model.WaitingOffer = this.ordersService.GetOrdersByState(OrderState.New).ToList().Select(o => Mapper.Map(o, new OrderViewModelSimple()));
			model.WaitingClientResponse = this.ordersService.GetOrdersByState(OrderState.WaitingClientResponse).ToList().Select(o => Mapper.Map(o, new OrderViewModelSimple()));
			model.Canceled = this.ordersService.GetOrdersByState(OrderState.Canceled).ToList().Select(o => Mapper.Map(o, new OrderViewModelSimple()));
			model.Done = this.ordersService.GetOrdersByState(OrderState.Done).ToList().Select(o => Mapper.Map(o, new OrderViewModelSimple()));

			return View(model);
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			Order dbOrder = this.ordersService.GetOrder(id);
			if (dbOrder != null)
			{
				EditOrderInputModel model = Mapper.Map(dbOrder, new EditOrderInputModel());
				model.SurfaceMaterials = this.materialsService.GetAllMaterials("surfaces").ToList();
				model.HandlesMaterials = this.materialsService.GetAllMaterials("handles").ToList();
				model.Categories = this.ordersService.GetCategoriesSelectData();
				return this.View(model);
			}

			return HttpNotFound();
		}

		[HttpPost]
		public ActionResult Edit(int id, EditOrderInputModel model)
		{
			if (ModelState.IsValid)
			{
				bool IsUpdateSuccessfull = this.ordersService.UpdateOrder(id, model);
				if (IsUpdateSuccessfull)
				{
					TempData["message"] = "Поръчката беше редактирана успешно!";
					TempData["messageType"] = "success";
					return RedirectToAction("Index");
				}
			}
			TempData["message"] = "Невалидни данни за поръчката!<br/> Моля попълнете <strong>всички</strong> задължителни полета!";
			TempData["messageType"] = "danger";
			return View(model);
		}

		[HttpPost]
		public ActionResult DeleteImage(int id)
		{
			if (this.imagesService.DeleteImage(id))
			{
				return this.Json(new { Status = "Success" });
			}
			else
			{
				return this.Json(new { Status = "Fail" });
			}
		}

		//TODO: Not done
		[HttpGet]
		public ActionResult NotifyClient(int id)
		{
			Order dbOrder = this.ordersService.GetOrder(id);

			if (dbOrder != null)
			{

			}

			NotifyClientInputModel model = new NotifyClientInputModel();
			return this.View(model);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			Order deletedOrder = this.ordersService.DeleteOrder(id);

			if (deletedOrder != null)
			{
				TempData["message"] = "Изтрито успешно!";
				TempData["messageType"] = "success";
				return this.RedirectToAction("Index");
			}

			return HttpNotFound();
		}
	}
}