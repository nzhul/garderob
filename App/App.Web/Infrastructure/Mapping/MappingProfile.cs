using App.Models;
using App.Models.Pages;
using App.Models.ViewModels;
using App.Web.Areas.Administration.Models.InputModels;
using AutoMapper;

namespace App.Web.Infrastructure.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Page, PageViewModel>();
			CreateMap<ApplicationUser, EditClientInputModel>();

			//Mapper.Initialize(cfg => cfg.CreateMap<Page, PageViewModel>());
			//Mapper.Initialize(cfg => cfg.CreateMap<ApplicationUser, EditClientInputModel>());
			//Mapper.Initialize(
			//	cfg => cfg.CreateMap<ApplicationUser, ApplicationUser>()
			//	.ForMember(x => x.Id, opt => opt.Ignore())
			//	.ForMember(x => x.PasswordHash, opt => opt.Ignore()));
		}
	}
}