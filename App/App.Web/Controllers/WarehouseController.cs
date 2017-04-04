using App.Data.Service.Abstraction;
using App.Models.Orders;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	[Authorize]
	public class WarehouseController : Controller
	{
		private IOrdersService ordersService;

		public WarehouseController(IOrdersService ordersService)
		{
			this.ordersService = ordersService;
		}

		public ActionResult Orders()
		{
			IList<Order> userOrders = this.ordersService.GetUserOrders(this.User.Identity.GetUserId()).ToList();
			IEnumerable<OrderViewModel> model = userOrders.Select(o => AutoMapper.Mapper.Map(o, new OrderViewModel()));
			return this.View(model);
		}
	}
}