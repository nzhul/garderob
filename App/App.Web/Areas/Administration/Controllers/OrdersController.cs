using App.Data.Service.Abstraction;
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

		public OrdersController(IOrdersService ordersService)
		{
			this.ordersService = ordersService;
		}

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
	}
}