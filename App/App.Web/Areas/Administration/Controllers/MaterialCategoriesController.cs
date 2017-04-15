using App.Data.Service.Abstraction;
using App.Models.Materials;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Utilities;

namespace App.Web.Areas.Administration.Controllers
{
	public class MaterialCategoriesController : BaseController
	{
		IMaterialsService materialsService;

		public MaterialCategoriesController(IMaterialsService materialsService)
		{
			this.materialsService = materialsService;
		}

		[HttpGet]
		public ActionResult Index()
		{
			IEnumerable<MaterialCategory> model = this.materialsService.GetAllCategoriesWithMaterials().ToList();
			return View(model);
		}

		[HttpGet]
		public ActionResult Create()
		{
			EditMaterialCategoryInputModel model = new EditMaterialCategoryInputModel();
			return View(model);
		}

		[HttpPost]
		public ActionResult Create(EditMaterialCategoryInputModel model)
		{
			if (!string.IsNullOrEmpty(model.Name))
			{
				model.Slug = SlugGenerator.Generate(model.Name);
				ModelState["Slug"].Errors.Clear();
				UpdateModel(model);
			}

			if (ModelState.IsValid)
			{
				int result = this.materialsService.CreateMaterialCategory(model);
				if (result > 0)
				{
					TempData["message"] = "Успешно добавихте нова категория!";
					TempData["messageType"] = "success";
					return RedirectToAction("Index");
				}
			}

			TempData["message"] = "Невалидни данни за категорията!<br/> Моля попълнете <strong>всички</strong> задължителни полета!";
			TempData["messageType"] = "danger";
			return View(model);
		}

		[HttpGet]
		public ActionResult Edit(int id)
		{
			MaterialCategory dbCategory = this.materialsService.GetMaterialCategory(id, true, true);
			EditMaterialCategoryInputModel model = Mapper.Map(dbCategory, new EditMaterialCategoryInputModel());
			return View(model);
		}

		[HttpPost]
		public ActionResult Edit(int id, EditMaterialCategoryInputModel model)
		{
			if (ModelState.IsValid)
			{
				MaterialCategory updatedCategory = this.materialsService.UpdateMaterialCategory(id, model);

				if (updatedCategory != null)
				{
					TempData["message"] = "Редактирахте успешно категорията!";
					TempData["messageType"] = "success";
					return RedirectToAction("Index");
				}
				else
				{
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
				}
			}

			TempData["message"] = "Невалидни данни за категорията!<br/> Моля попълнете <strong>всички</strong> полета в червено!";
			TempData["messageType"] = "danger";
			return View(model);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			MaterialCategory deletedCategory = this.materialsService.DeleteMaterialCategory(id);

			if (deletedCategory != null)
			{
				TempData["message"] = "Изтрито успешно!";
				TempData["messageType"] = "success";
				return this.RedirectToAction("Index");
			}

			return HttpNotFound();
		}
	}
}