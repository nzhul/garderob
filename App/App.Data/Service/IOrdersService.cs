using App.Models.Images;
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

		int MakeOrder(OrderInputModel model);

		void AddSketchImage(int orderId, Image image, bool notifyClient, bool notifyAdmin);

		void AddDesignImage(int orderId, Image image, bool notifyClient, bool notifyAdmin);

		void AddResultImage(int orderId, Image image, bool notifyClient, bool notifyAdmin);

		void ChangeOrderState(int orderId, OrderState newState, bool notifyClient, bool notifyAdmin);
	}
}
