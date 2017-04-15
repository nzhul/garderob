﻿using App.Models.Materials;
using System.Linq;
using System.Collections.Generic;
using App.Models.InputModels;
using System.Web.Mvc;

namespace App.Data.Service.Abstraction
{
	public interface IMaterialsService
	{
		int CreateMaterial(EditMaterialInputModel model);

		Material DeleteMaterial(int id);

		Material UpdateMaterial(int id, EditMaterialInputModel model);

		IQueryable<Material> GetAllMaterials(string materialCategorySlug);

		Material GetMaterial(int id);

		IQueryable<MaterialCategory> GetAllCategoriesWithMaterials();

		IQueryable<MaterialCategory> GetAllCategories();

		int CreateMaterialCategory(EditMaterialCategoryInputModel categoryInput);

		MaterialCategory GetMaterialCategory(int id, bool includeImage, bool includePdf);

		MaterialCategory UpdateMaterialCategory(int id, EditMaterialCategoryInputModel model);

		IEnumerable<SelectListItem> GetCategoriesSelectData();

		MaterialCategory DeleteMaterialCategory(int id);
	}
}