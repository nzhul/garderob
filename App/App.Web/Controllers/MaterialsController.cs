using App.Data.Service.Abstraction;
using App.Models.Materials;
using System.Linq;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class MaterialsController : Controller
	{
		private IMaterialsService materialsService;

		public MaterialsController(IMaterialsService materialsService)
		{
			this.materialsService = materialsService;
		}

		[HttpGet]
		public ActionResult List(int id)
		{
			MaterialCategory model = this.materialsService.GetMaterialCategory(id, false, false);
			model.Materials = model.Materials.Where(x => x.IsDisabled != true).ToList();

			if (model != null)
			{
				return this.View(model);
			}

			return this.HttpNotFound();
		}
	}
}