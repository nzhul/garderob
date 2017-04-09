using App.Data.Service.Abstraction;
using App.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class ProductsController : Controller
	{
		private IOrdersService ordersService;

		public ProductsController(IOrdersService ordersService)
		{
			this.ordersService = ordersService;
		}

		public ActionResult List()
		{
			ProductGalleryViewModel model = new ProductGalleryViewModel();

			IEnumerable<Order> allDoneOrders = this.ordersService.GetAllDoneOrders().ToList();

			model.Categories = this.GetCategories(allDoneOrders);
			model.Products = allDoneOrders.Select(o => AutoMapper.Mapper.Map(o, new ProductViewModel()));

			return View(model);
		}

		private IEnumerable<CategoryItem> GetCategories(IEnumerable<Order> allDoneOrders)
		{
			IList<CategoryItem> categories = new List<CategoryItem>();

			foreach (Order order in allDoneOrders)
			{
				if (!this.CategoryIsAdded(categories, order.OrderCategory))
				{
					categories.Add(new CategoryItem { Slug = order.OrderCategory.Slug, Title = order.OrderCategory.Name });
				}
			}

			return categories;
		}

		private bool CategoryIsAdded(IEnumerable<CategoryItem> categories, OrderCategory orderCategory)
		{
			foreach (var category in categories)
			{
				if (category.Slug == orderCategory.Slug && category.Title == orderCategory.Name)
				{
					return true;
				}
			}

			return false;
		}
	}
}