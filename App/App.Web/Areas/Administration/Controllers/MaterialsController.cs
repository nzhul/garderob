using App.Data.Service.Abstraction;
using App.Models.Materials;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Areas.Administration.Controllers
{
	public class MaterialsController : BaseController
	{
		private IMaterialsService materialsService;

		public MaterialsController(IMaterialsService materialsService)
		{
			this.materialsService = materialsService;
		}

		[HttpGet]
		public ActionResult Index()
		{
			IEnumerable<MaterialCategory> model = this.materialsService.GetAllCategoriesWithMaterials().ToList();

			return this.View(model);
		}
	}
}