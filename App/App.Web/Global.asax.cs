using App.Models;
using App.Models.Pages;
using App.Models.ViewModels;
using App.Utilities;
using App.Web.Infrastructure.ControllerFactory;
using AutoMapper;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace App.Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			ModelBinders.Binders.Add(typeof(DateTime), new BulgarianTimeModelBinder());

			Mapper.Initialize(cfg => cfg.CreateMap<Page, PageViewModel>());
			//Mapper.Initialize(
			//	cfg => cfg.CreateMap<ApplicationUser, ApplicationUser>()
			//	.ForMember(x => x.Id, opt => opt.Ignore())
			//	.ForMember(x => x.PasswordHash, opt => opt.Ignore()));
		}
	}
}