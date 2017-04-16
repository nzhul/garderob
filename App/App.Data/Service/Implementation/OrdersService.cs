using App.Data.Service.Abstraction;
using App.Data.Service.Messaging;
using App.Data.Utilities;
using App.Models;
using App.Models.Images;
using App.Models.InputModels;
using App.Models.Orders;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utilities;
using System.Web.Mvc;

namespace App.Data.Service.Implementation
{
	public class OrdersService : IOrdersService
	{
		private readonly IUoWData Data;
		private readonly IMessagingService MessagingService;
		//private const string defaultBigImageQuery = "width=1650&height=1050&crop=auto&scale=both&format=jpg";
		private const string defaultBigImageQuery = "width=1650&height=1050&format=jpg";
		private const string defaultMediumImageQuery = "width=370&height=310&crop=auto&format=jpg";
		private const string defaultSmallImageQuery = "width=210&height=203&crop=auto&format=jpg";

		public OrdersService(IUoWData data, IMessagingService messagingService)
		{
			this.Data = data;
			this.MessagingService = messagingService;
		}

		public Order DeleteOrder(int id)
		{
			Order dbOrder = this.Data.Orders.Find(id);

			if (dbOrder != null)
			{
				foreach (var image in dbOrder.SketchImages.ToList())
				{
					this.Data.Images.Delete(image);
					this.Data.SaveChanges();
				}

				foreach (var image in dbOrder.DesignImages.ToList())
				{
					this.Data.Images.Delete(image);
					this.Data.SaveChanges();
				}

				foreach (var image in dbOrder.ResultImages.ToList())
				{
					this.Data.Images.Delete(image);
					this.Data.SaveChanges();
				}

				foreach (var testimonial in dbOrder.Testimonials.ToList())
				{
					this.Data.Testimonials.Delete(testimonial);
					this.Data.SaveChanges();
				}

				this.Data.Orders.Delete(dbOrder);
				this.Data.SaveChanges();
			}

			return dbOrder;
		}

		public bool DeleteOrderCategory(int id)
		{
			OrderCategory dbCategory = this.Data.OrderCategories.Find(id);

			if (dbCategory == null)
			{
				return false;
			}

			this.Data.OrderCategories.Delete(id);
			this.Data.SaveChanges();
			return true;
		}

		public Order GetOrder(int id)
		{
			return this.Data.Orders.Find(id);
		}

		public IQueryable<OrderCategory> GetOrderCategories()
		{
			return this.Data.OrderCategories.All();
		}

		public OrderCategory GetOrderCategoryById(int id)
		{
			return this.Data.OrderCategories.Find(id);
		}

		public OrderCategory GetOrderCategoryBySlug(string slug)
		{
			return this.Data.OrderCategories.All().Where(c => c.Slug == slug).FirstOrDefault();
		}

		public IQueryable<Order> GetOrdersByCategory(string category)
		{
			return this.Data.Orders.All().Where(o => o.OrderCategory.Name == category);
		}

		public IQueryable<Order> GetUserOrders(string userId)
		{
			return this.Data.Orders.All().Where(o => o.Client.Id == userId).OrderByDescending(o => o.LastModified);
		}

		public IQueryable<Order> GetUserCart(string userId)
		{
			ApplicationUser dbUser = this.Data.Users.Find(userId);

			if (dbUser != null)
			{
				return dbUser.Cart.AsQueryable();
			}
			else
			{
				throw new ArgumentNullException("Cannot find cart for user with ID: " + userId);
			}
		}

		public int MakeOrder(OrderInputModel model)
		{
			Order newOrder = new Order();
			newOrder = Mapper.Map(model, newOrder);
			newOrder.Slug = SlugGenerator.Generate(model.Title);
			newOrder.RequestDate = DateTime.UtcNow;
			newOrder.OfferDate = DateTime.MaxValue;
			newOrder.CompleteDate = DateTime.MaxValue;
			newOrder.LastModified = DateTime.UtcNow;
			newOrder.Count = 1;

			this.Data.Orders.Add(newOrder);
			this.Data.SaveChanges();

			foreach (HttpPostedFileBase image in model.PostedSketches)
			{
				byte[] bigImageData = ImageUtilities.CropImage(image, OrdersService.defaultBigImageQuery);
				byte[] mediumImageData = ImageUtilities.CropImage(image, OrdersService.defaultMediumImageQuery);
				byte[] smallImageData = ImageUtilities.CropImage(image, OrdersService.defaultSmallImageQuery);

				Image newSketch = new Image
				{
					Big = bigImageData,
					Medium = mediumImageData,
					Small = smallImageData
				};

				newOrder.SketchImages.Add(newSketch);
			}

			this.Data.SaveChanges();

			//TODO: send admin notification!!

			return newOrder.Id;
		}

		public OrderCategory UpdateOrderCategory(int id, EditOrderCategoryInputModel inputModel)
		{
			OrderCategory dbCategory = this.Data.OrderCategories.Find(id);
			if (dbCategory != null)
			{
				dbCategory = Mapper.Map(inputModel, dbCategory);
				dbCategory.Slug = SlugGenerator.Generate(inputModel.Name);
				dbCategory.LastModified = DateTime.UtcNow;
				this.Data.SaveChanges();
			}

			return dbCategory;
		}

		public Order AddCartItem(int orderId, int orderCount, bool installation, string userId)
		{
			Order dbOrder = this.Data.Orders.Find(orderId);
			ApplicationUser dbUser = this.Data.Users.Find(userId);

			if (dbOrder != null && dbUser != null && dbOrder.Client.Id == dbUser.Id)
			{
				dbOrder.IsInCart = true;
				dbOrder.Count = orderCount;
				dbOrder.Installation = installation;
				dbUser.Cart.Add(dbOrder);
				this.Data.SaveChanges();
				return dbOrder;
			}
			else
			{
				return null;
			}
		}

		public bool RemoveCartItem(int orderId, string userId)
		{
			Order dbOrder = this.Data.Orders.Find(orderId);
			ApplicationUser dbUser = this.Data.Users.Find(userId);

			if (dbOrder != null && dbUser != null && dbOrder.Client.Id == dbUser.Id)
			{
				dbOrder.IsInCart = false;
				dbUser.Cart.Remove(dbOrder);
				this.Data.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool OrderNow(string userId)
		{
			// get all items from applicationuser.cart
			// remove them from cart collection
			// set their status to InProduction
			// Send notifications to both client and administrator

			ApplicationUser currentUser = this.Data.Users.Find(userId);

			if (currentUser != null)
			{
				ICollection<Order> cartItems = currentUser.Cart;
				//this.SendOrderSuccessMessage(userId, cartItems);
				//this.SendAdminNotification("adminId", cartItems);

				foreach (Order order in cartItems)
				{
					order.IsInCart = false;
					order.State = OrderState.InProduction;
					this.Data.SaveChanges();
				}

				currentUser.Cart.ToList().ForEach(x => currentUser.Cart.Remove(x));
				this.Data.SaveChanges();

				return true;
			}

			return false;
		}

		private void SendAdminNotification(string adminId, ICollection<Order> cartItems)
		{
			MessageData message = new MessageData();

			foreach (Order order in cartItems)
			{

			}

			this.MessagingService.Notify(adminId, message);
		}

		private void SendOrderSuccessMessage(string userId, ICollection<Order> cartItems)
		{
			MessageData message = new MessageData();

			foreach (Order order in cartItems)
			{

			}

			this.MessagingService.Notify(userId, message);
		}

		public IQueryable<Order> GetAllDoneOrders()
		{
			return this.Data.Orders.All().Where(o => o.State == OrderState.Done);
		}

		public IQueryable<Order> GetOrdersByState(OrderState state)
		{
			return this.Data.Orders.All().Where(o => o.State == state);
		}

		public bool UpdateOrder(int id, EditOrderInputModel model)
		{
			Order dbOrder = this.GetOrder(id);
			if (dbOrder != null)
			{
				dbOrder = Mapper.Map(model, dbOrder);
				dbOrder.Slug = SlugGenerator.Generate(model.Title);

				if (model.PostedSketches != null && model.PostedSketches.Count > 0 && model.PostedSketches[0] != null)
				{
					foreach (HttpPostedFileBase image in model.PostedSketches)
					{
						byte[] smallImageData = ImageUtilities.CropImage(image, OrdersService.defaultSmallImageQuery);
						byte[] mediumImageData = ImageUtilities.CropImage(image, OrdersService.defaultMediumImageQuery);
						byte[] bigImageData = ImageUtilities.CropImage(image, OrdersService.defaultBigImageQuery);

						Image newImage = new Image
						{
							Small = smallImageData,
							Medium = mediumImageData,
							Big = bigImageData
						};

						dbOrder.SketchImages.Add(newImage);
					}
				}

				if (model.PostedDesigns != null && model.PostedDesigns.Count > 0 && model.PostedDesigns[0] != null)
				{
					foreach (HttpPostedFileBase image in model.PostedDesigns)
					{
						byte[] smallImageData = ImageUtilities.CropImage(image, OrdersService.defaultSmallImageQuery);
						byte[] mediumImageData = ImageUtilities.CropImage(image, OrdersService.defaultMediumImageQuery);
						byte[] bigImageData = ImageUtilities.CropImage(image, OrdersService.defaultBigImageQuery);

						Image newImage = new Image
						{
							Small = smallImageData,
							Medium = mediumImageData,
							Big = bigImageData
						};

						dbOrder.DesignImages.Add(newImage);
					}
				}

				if (model.PostedResults != null && model.PostedResults.Count > 0 && model.PostedResults[0] != null)
				{
					foreach (HttpPostedFileBase image in model.PostedResults)
					{
						byte[] smallImageData = ImageUtilities.CropImage(image, OrdersService.defaultSmallImageQuery);
						byte[] mediumImageData = ImageUtilities.CropImage(image, OrdersService.defaultMediumImageQuery);
						byte[] bigImageData = ImageUtilities.CropImage(image, OrdersService.defaultBigImageQuery);

						Image newImage = new Image
						{
							Small = smallImageData,
							Medium = mediumImageData,
							Big = bigImageData
						};

						dbOrder.ResultImages.Add(newImage);
					}
				}

				if (model.State == OrderState.Done)
				{
					dbOrder.CompleteDate = DateTime.UtcNow;
				}

				if (model.State == OrderState.WaitingClientResponse)
				{
					dbOrder.OfferDate = DateTime.UtcNow;
				}


				dbOrder.LastModified = DateTime.UtcNow;
				this.Data.SaveChanges();

				return true;
			}
			else
			{
				return false;
			}
		}

		public int CreateOrderCategory(EditOrderCategoryInputModel model)
		{
			OrderCategory newCategory = Mapper.Map(model, new OrderCategory());
			newCategory.DateCreated = DateTime.UtcNow;
			newCategory.LastModified = DateTime.UtcNow;
			newCategory.Slug = SlugGenerator.Generate(model.Name);

			this.Data.OrderCategories.Add(newCategory);
			this.Data.SaveChanges();

			return newCategory.Id;

		}

		public IEnumerable<SelectListItem> GetCategoriesSelectData()
		{
			var categories = this.Data.OrderCategories
				.All()
				.OrderBy(x => x.Id)
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Name.ToString()
				});

			return new SelectList(categories, "Value", "Text");
		}
	}
}