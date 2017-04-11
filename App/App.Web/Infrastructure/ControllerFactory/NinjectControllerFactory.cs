using App.Data;
using App.Data.Service.Abstraction;
using App.Data.Service.Implementation;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Web.Infrastructure.ControllerFactory
{
	public class NinjectControllerFactory : DefaultControllerFactory
	{
		private IKernel ninjectKernel;

		public NinjectControllerFactory()
		{
			this.ninjectKernel = new StandardKernel();
			this.AddBindings();
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
		}

		private void AddBindings()
		{
			ninjectKernel.Bind<IUoWData>().To<UoWData>();
			ninjectKernel.Bind<IClientsService>().To<ClientsService>();
			ninjectKernel.Bind<IPagesService>().To<PagesService>();
			ninjectKernel.Bind<IOrdersService>().To<OrdersService>();
			ninjectKernel.Bind<IMaterialsService>().To<MaterialsService>();
			ninjectKernel.Bind<ITestimonialsService>().To<TestimonialsService>();
			ninjectKernel.Bind<IImagesService>().To<ImagesService>();
			ninjectKernel.Bind<IMessagingService>().To<App.Data.Service.Implementation.EmailService>();
		}
	}
}