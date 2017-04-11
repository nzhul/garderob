using App.Models.InputModels;
using App.Models.Orders;
using System.Linq;

namespace App.Data.Service.Abstraction
{
	public interface IOrdersService
	{
		IQueryable<Order> GetUserOrders(string userId);

		IQueryable<Order> GetUserCart(string userId);

		IQueryable<Order> GetOrdersByCategory(string category);

		Order GetOrder(int id);

		bool DeleteOrder(int id);

		IQueryable<OrderCategory> GetOrderCategories();

		IQueryable<Order> GetOrdersByState(OrderState state);

		OrderCategory GetOrderCategoryById(int id);

		OrderCategory GetOrderCategoryBySlug(string slug);

		IQueryable<Order> GetAllDoneOrders();

		bool UpdateOrderCategory(int id, OrderCategoryInputModel inputModel);

		bool DeleteOrderCategory(int id); // do cannonical delete for all orders in that category OR transfer all orders into "Unknown" category

		int MakeOrder(OrderInputModel model);

		//TODO: delete
		//bool ConfirmOrderOffer(int orderId, int count, bool notifyAdmin);

		//TODO: delete
		//void AddSketchImage(int orderId, Image image, bool notifyClient, bool notifyAdmin);

		//TODO: delete
		//void AddDesignImage(int orderId, Image image, bool notifyClient, bool notifyAdmin);

		//TODO: delete
		//void AddResultImage(int orderId, Image image, bool notifyClient, bool notifyAdmin);

		//TODO: delete
		//void ChangeOrderState(int orderId, OrderState newState, bool notifyClient, bool notifyAdmin);

		Order AddCartItem(int orderId, int orderCount, bool installation, string userId);

		bool RemoveCartItem(int orderId, string userId);

		bool OrderNow(string userId);

		bool UpdateOrder(int id, EditOrderInputModel model);
	}
}