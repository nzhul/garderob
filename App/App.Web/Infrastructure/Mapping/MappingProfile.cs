using App.Models;
using App.Models.InputModels;
using App.Models.Pages;
using App.Models.ViewModels;
using AutoMapper;

namespace App.Web.Infrastructure.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Page, PageViewModel>();
			CreateMap<ApplicationUser, EditClientInputModel>().ForMember(x => x.Phone, opt => opt.MapFrom(u => u.PhoneNumber));
			CreateMap<EditClientInputModel, ApplicationUser>().ForMember(x => x.PhoneNumber, opt => opt.MapFrom(u => u.Phone));

			//Mapper.Initialize(cfg => cfg.CreateMap<Page, PageViewModel>());
			//Mapper.Initialize(cfg => cfg.CreateMap<ApplicationUser, EditClientInputModel>());
			//Mapper.Initialize(
			//	cfg => cfg.CreateMap<ApplicationUser, ApplicationUser>()
			//	.ForMember(x => x.Id, opt => opt.Ignore())
			//	.ForMember(x => x.PasswordHash, opt => opt.Ignore()));
		}
	}
}