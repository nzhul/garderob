using App.Models;
using App.Models.InputModels;
using App.Models.Materials;
using App.Models.Orders;
using App.Models.Pages;
using App.Models.Testimonials;
using App.Models.ViewModels;
using AutoMapper;
using System;
using System.Linq;

namespace App.Web.Infrastructure.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Page, PageViewModel>();
			CreateMap<ApplicationUser, EditClientInputModel>().ForMember(x => x.Phone, opt => opt.MapFrom(u => u.PhoneNumber));

			CreateMap<EditClientInputModel, ApplicationUser>()
				.ForMember(x => x.PhoneNumber, opt => opt.MapFrom(u => u.Phone))
				.ForMember(x => x.ProfileImage, opt => opt.Ignore());

			CreateMap<OrderInputModel, Order>().ForMember(x => x.Slug, opt => opt.MapFrom(u => u.Title.ToLower()));
			CreateMap<Order, OrderViewModel>()
				.ForMember(x => x.SketchImage, opt => opt.MapFrom(u => u.SketchImages.FirstOrDefault().Small))
				.ForMember(x => x.DesignImage, opt => opt.MapFrom(u => u.DesignImages.FirstOrDefault().Small))
				.ForMember(x => x.ResultImage, opt => opt.MapFrom(u => u.ResultImages.FirstOrDefault().Small));

			CreateMap<Order, OrderViewModelSimple>()
				.ForMember(x => x.ClientFullName, opt => opt.MapFrom(u => u.Client.FirstName + " " + u.Client.LastName))
				.ForMember(x => x.ClientId, opt => opt.MapFrom(u => u.Client.Id));

			CreateMap<Order, ProductViewModel>()
				.ForMember(x => x.ResultImageSmall, opt => opt.MapFrom(u => u.ResultImages.FirstOrDefault().Medium))
				.ForMember(x => x.ResultImageBig, opt => opt.MapFrom(u => u.ResultImages.FirstOrDefault().Big))
				.ForMember(x => x.CategorySlug, opt => opt.MapFrom(u => u.OrderCategory.Slug));

			CreateMap<Order, EditOrderInputModel>()
				.ForMember(x => x.ClientFullName, opt => opt.MapFrom(u => u.Client.FirstName + " " + u.Client.LastName))
				.ForMember(x => x.SelectedCategoryId, opt => opt.MapFrom(u => u.OrderCategoryId));

			CreateMap<EditOrderInputModel, Order>()
				.ForMember(x => x.SketchImages, opt => opt.Ignore())
				.ForMember(x => x.DesignImages, opt => opt.Ignore())
				.ForMember(x => x.ResultImages, opt => opt.Ignore())
				.ForMember(x => x.RequestDate, opt => opt.Ignore())
				.ForMember(x => x.ClientId, opt => opt.Ignore())
				.ForMember(x => x.OrderCategoryId, opt => opt.MapFrom(u => u.SelectedCategoryId));

			CreateMap<Testimonial, TestimonialViewModel>()
				.ForMember(x => x.OrderSketch, opt => opt.MapFrom(u => u.Order.SketchImages.FirstOrDefault()))
				.ForMember(x => x.OrderDesign, opt => opt.MapFrom(u => u.Order.DesignImages.FirstOrDefault()))
				.ForMember(x => x.OrderResult, opt => opt.MapFrom(u => u.Order.ResultImages.FirstOrDefault()))
				.ForMember(x => x.ClientPhoto, opt => opt.MapFrom(u => u.Client.ProfileImage))
				.ForMember(x => x.ClientFullName, opt => opt.MapFrom(u => u.Client.FirstName + " " + u.Client.LastName))
				.ForMember(x => x.ClientJobTitle, opt => opt.MapFrom(u => u.Client.JobTitle));

			CreateMap<Testimonial, TestimonialSimpleViewModel>()
				.ForMember(x => x.ClientFullName, opt => opt.MapFrom(u => u.Client.FirstName + " " + u.Client.LastName))
				.ForMember(x => x.OrderTitle, opt => opt.MapFrom(u => u.Order.Title));

			CreateMap<EditTestimonialInputModel, Testimonial>()
				.ForMember(x => x.SubmissionDate, opt => opt.Ignore())
				.ForMember(x => x.OrderId, opt => opt.Ignore())
				.ForMember(x => x.ClientId, opt => opt.Ignore());


			CreateMap<Testimonial, EditTestimonialInputModel>()
				.ForMember(x => x.ClientFullName, opt => opt.MapFrom(u => u.Client.FirstName + " " + u.Client.LastName))
				.ForMember(x => x.OrderTitle, opt => opt.MapFrom(u => u.Order.Title));

			CreateMap<OrderCategory, OrderCategoryViewModel>();
			CreateMap<OrderCategory, EditOrderCategoryInputModel>();
			CreateMap<EditOrderCategoryInputModel, OrderCategory>();

			CreateMap<MaterialCategory, EditMaterialCategoryInputModel>();

			CreateMap<EditMaterialCategoryInputModel, MaterialCategory>()
				.ForMember(x => x.Image, opt => opt.Ignore())
				.ForMember(x => x.Pdf, opt => opt.Ignore());

			CreateMap<Material, EditMaterialInputModel>()
				.ForMember(x => x.SmallImageSize, opt => opt.MapFrom(u => u.Category.SmallImageSize))
				.ForMember(x => x.MediumImageSize, opt => opt.MapFrom(u => u.Category.MediumImageSize))
				.ForMember(x => x.BigImageSize, opt => opt.MapFrom(u => u.Category.BigImageSize))
				.ForMember(x => x.SelectedCategoryId, opt => opt.MapFrom(u => u.CategoryId));

			CreateMap<EditMaterialInputModel, Material>()
				.ForMember(x => x.DateCreated, opt => opt.Ignore())
				.ForMember(x => x.LastModified, opt => opt.Ignore())
				.ForMember(x => x.Image, opt => opt.Ignore())
				.ForMember(x => x.CategoryId, opt => opt.MapFrom(u => u.SelectedCategoryId));

			//TODO: use slugify for OrderInputModel to Order mapping -> Slug
			// http://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
		}
	}
}