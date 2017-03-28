using App.Models.Orders;
using System.Linq;

namespace App.Data.Service
{
	public interface IOrdersService
	{
		IQueryable<Order> GetOrdersByUserId(string userId);

		IQueryable<Order> GetOrdersByCategory(string category);

		Order GetOrderById(int id);

		bool UpdateOrder(int id, OrderInputModel inputModel);

		bool DeleteOrder(int id);

		IQueryable<OrderCategory> GetOrderCategories();

		OrderCategory GetOrderCategoryById(int id);

		bool UpdateOrderCategory(int id, OrderCategoryInputModel inputModel);

		bool DeleteOrderCategory(int id); // do cannonical delete for all orders in that category OR transfer all orders into "Unknown" category
	}
}
