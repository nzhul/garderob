using App.Data.Service.Abstraction;
using App.Models.InputModels;
using App.Models.Materials;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Utilities;

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

		[HttpGet]
		public ActionResult Create(int id)
		{
			EditMaterialInputModel model = new EditMaterialInputModel();
			model.Categories = this.materialsService.GetCategoriesSelectData();
			model.SelectedCategoryId = id;

			MaterialCategory selectedCategory = this.materialsService.GetMaterialCategory(id, false, false);
			model.SmallImageSize = selectedCategory.SmallImageSize;
			model.MediumImageSize = selectedCategory.MediumImageSize;
			model.BigImageSize = selectedCategory.BigImageSize;

			return View(model);
		}

		[HttpPost]
		public ActionResult Create(int id, EditMaterialInputModel model)
		{
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Slug = SlugGenerator.Generate(model.Name);
				ModelState["Slug"].Errors.Clear();
				UpdateModel(model);
			}

			if (ModelState.IsValid)
			{
				int result = this.materialsService.CreateMaterial(model);
				if (result > 0)
				{
					TempData["message"] = "Успешно добавихте нов материал!";
					TempData["messageType"] = "success";
					return RedirectToAction("Index");
				}
			}

			model.Categories = this.materialsService.GetCategoriesSelectData();
			model.SelectedCategoryId = id;

			MaterialCategory selectedCategory = this.materialsService.GetMaterialCategory(id, false, false);
			model.SmallImageSize = selectedCategory.SmallImageSize;
			model.MediumImageSize = selectedCategory.MediumImageSize;
			model.BigImageSize = selectedCategory.BigImageSize;

			TempData["message"] = "Невалидни данни!<br/> Моля попълнете <strong>всички</strong> задължителни полета!";
			TempData["messageType"] = "danger";
			return View(model);
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			Material dbCategory = this.materialsService.GetMaterial(id);
			EditMaterialInputModel model = Mapper.Map(dbCategory, new EditMaterialInputModel());
			model.Categories = this.materialsService.GetCategoriesSelectData();
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(int id, EditMaterialInputModel model)
		{
			if (ModelState.IsValid)
			{
				Material updatedMaterial = this.materialsService.UpdateMaterial(id, model);

				if (updatedMaterial != null)
				{
					TempData["message"] = "Редактирахте успешно материалта!";
					TempData["messageType"] = "success";
					return RedirectToAction("Index");
				}
				else
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}

			model.Categories = this.materialsService.GetCategoriesSelectData();
			TempData["message"] = "Невалидни данни за материалта!<br/> Моля попълнете <strong>всички</strong> полета в червено!";
			TempData["messageType"] = "danger";
			return View(model);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			Material deletedMaterial = this.materialsService.DeleteMaterial(id);

			if (deletedMaterial != null)
			{
				TempData["message"] = "Изтрито успешно!";
				TempData["messageType"] = "success";
				return this.RedirectToAction("Index");
			}

			return HttpNotFound();
		}
	}
}