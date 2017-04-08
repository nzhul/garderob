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
			IList<Order> dbOrders = this.ordersService.GetUserOrders(this.User.Identity.GetUserId()).ToList();
			IEnumerable<OrderViewModel> orders = dbOrders.Select(o => AutoMapper.Mapper.Map(o, new OrderViewModel()));

			IList<Order> dbCart = this.ordersService.GetUserCart(this.User.Identity.GetUserId()).ToList();
			IEnumerable<OrderViewModel> cart = dbCart.Select(o => AutoMapper.Mapper.Map(o, new OrderViewModel()));

			CartViewModel model = new CartViewModel
			{
				Orders = orders,
				Cart = cart
			};

			return this.View(model);
		}

		[HttpPost]
		public ActionResult AddCartItem(int orderId, int orderCount, bool installation)
		{
			//TODO: remove the threading

			System.Threading.Thread.Sleep(2500);

			string userId = this.User.Identity.GetUserId();
			Order dbOrder = this.ordersService.AddCartItem(orderId, orderCount, installation, userId);
			if (dbOrder != null)
			{
				return Json(
					new
					{
						Status = "Success",
						Data = new
						{
							id = dbOrder.Id,
							count = dbOrder.Count,
							title = dbOrder.Title,
							installation = dbOrder.Installation,
							price = dbOrder.Price,
						}
					});
			}
			else
			{
				return Json(new { Status = "Fail" });
			}
		}

		[HttpPost]
		public ActionResult RemoveCartItem(int orderId)
		{
			string userId = this.User.Identity.GetUserId();
			if (this.ordersService.RemoveCartItem(orderId, userId))
			{
				return Json(new { Status = "Success" });
			}
			else
			{
				// order or client was not found, or the order do not belong to the user ( hack )
				return Json(new { Status = "Fail" });
			}
		}
	}
}