using App.Models;
using App.Models.InputModels;
using App.Models.Orders;
using App.Models.Pages;
using App.Models.ViewModels;
using AutoMapper;
using System.Linq;

namespace App.Web.Infrastructure.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Page, PageViewModel>();
			CreateMap<ApplicationUser, EditClientInputModel>().ForMember(x => x.Phone, opt => opt.MapFrom(u => u.PhoneNumber));
			CreateMap<EditClientInputModel, ApplicationUser>().ForMember(x => x.PhoneNumber, opt => opt.MapFrom(u => u.Phone));
			CreateMap<OrderInputModel, Order>().ForMember(x => x.Slug, opt => opt.MapFrom(u => u.Title.ToLower()));
			CreateMap<Order, OrderViewModel>()
				.ForMember(x => x.SketchImage, opt => opt.MapFrom(u => u.SketchImages.FirstOrDefault().Small))
				.ForMember(x => x.DesignImage, opt => opt.MapFrom(u => u.DesignImages.FirstOrDefault().Small))
				.ForMember(x => x.ResultImage, opt => opt.MapFrom(u => u.ResultImages.FirstOrDefault().Small));

			CreateMap<Order, ProductViewModel>()
				.ForMember(x => x.ResultImageSmall, opt => opt.MapFrom(u => u.ResultImages.FirstOrDefault().Small))
				.ForMember(x => x.ResultImageBig, opt => opt.MapFrom(u => u.ResultImages.FirstOrDefault().Big));

			//TODO: use slugify for OrderInputModel to Order mapping -> Slug
			// http://stackoverflow.com/questions/2920744/url-slugify-algorithm-in-c
		}
	}
}