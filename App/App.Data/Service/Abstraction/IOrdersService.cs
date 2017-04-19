using App.Models.InputModels;
using App.Models.Orders;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace App.Data.Service.Abstraction
{
	public interface IOrdersService
	{
		IQueryable<Order> GetUserOrders(string userId);

		IQueryable<Order> GetUserCart(string userId);

		IQueryable<Order> GetOrdersByCategory(string category);

		Order GetOrder(int id);

		Order DeleteOrder(int id);

		IQueryable<OrderCategory> GetOrderCategories();

		IQueryable<Order> GetOrdersByState(OrderState state);

		OrderCategory GetOrderCategoryById(int id);

		OrderCategory GetOrderCategoryBySlug(string slug);

		IQueryable<Order> GetAllDoneOrders();

		OrderCategory UpdateOrderCategory(int id, EditOrderCategoryInputModel inputModel);

		bool DeleteOrderCategory(int id); // do cannonical delete for all orders in that category OR transfer all orders into "Unknown" category

		int MakeOrder(OrderInputModel model);

		int CreateOrderCategory(EditOrderCategoryInputModel categoryInput);

		Order AddCartItem(int orderId, int orderCount, bool installation, string userId);

		bool RemoveCartItem(int orderId, string userId);

		bool OrderNow(string userId);

		bool UpdateOrder(int id, EditOrderInputModel model);

		IEnumerable<SelectListItem> GetCategoriesSelectData();

		bool TryAssignUserToOrders(string email);
	}
}