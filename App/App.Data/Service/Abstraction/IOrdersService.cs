using App.Models.Images;
using App.Models.Orders;
using System.Linq;
using System.Collections.Generic;
using App.Models.Testimonials;

namespace App.Data.Service.Abstraction
{
	public interface IOrdersService
	{
		IQueryable<Order> GetUserOrders(string userId);

		IQueryable<Order> GetUserCart(string userId);

		IQueryable<Order> GetOrdersByCategory(string category);

		Order GetOrder(int id);

		bool UpdateOrder(int id, OrderInputModel inputModel);

		bool DeleteOrder(int id);

		IQueryable<OrderCategory> GetOrderCategories();

		OrderCategory GetOrderCategoryById(int id);

		OrderCategory GetOrderCategoryBySlug(string slug);

		IQueryable<Order> GetAllDoneOrders();

		bool UpdateOrderCategory(int id, OrderCategoryInputModel inputModel);

		bool DeleteOrderCategory(int id); // do cannonical delete for all orders in that category OR transfer all orders into "Unknown" category

		int MakeOrder(OrderInputModel model);

		bool ConfirmOrderOffer(int orderId, int count, bool notifyAdmin);

		void AddSketchImage(int orderId, Image image, bool notifyClient, bool notifyAdmin);

		void AddDesignImage(int orderId, Image image, bool notifyClient, bool notifyAdmin);

		void AddResultImage(int orderId, Image image, bool notifyClient, bool notifyAdmin);

		void ChangeOrderState(int orderId, OrderState newState, bool notifyClient, bool notifyAdmin);

		Order AddCartItem(int orderId, int orderCount, bool installation, string userId);

		bool RemoveCartItem(int orderId, string userId);

		bool OrderNow(string userId);

		Testimonial AddTestimonial(TestimonialInputModel model, string userId);
	}
}