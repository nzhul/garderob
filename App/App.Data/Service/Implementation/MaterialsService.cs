using System;
using System.Linq;
using App.Data.Service.Abstraction;
using App.Models.Materials;
using System.Data.Entity;
using App.Models.InputModels;
using AutoMapper;
using Utilities;

namespace App.Data.Service.Implementation
{
	public class MaterialsService : IMaterialsService
	{
		private IUoWData Data;

		public MaterialsService(IUoWData data)
		{
			this.Data = data;
		}

		public int CreateMaterial(MaterialInputModel model)
		{
			throw new NotImplementedException();
		}

		public int CreateMaterialCategory(EditMaterialCategoryInputModel model)
		{
			MaterialCategory newCategory = Mapper.Map(model, new MaterialCategory());
			newCategory.DateCreated = DateTime.UtcNow;
			newCategory.LastModified = DateTime.UtcNow;
			newCategory.Slug = SlugGenerator.Generate(model.Name);

			this.Data.MaterialCategories.Add(newCategory);
			this.Data.SaveChanges();

			return newCategory.Id;
		}

		public int CreateSurfaceMaterial(SurfaceMaterialInputModel model)
		{
			throw new NotImplementedException();
		}

		public bool DeleteMaterial(int id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<MaterialCategory> GetAllCategoriesWithMaterials()
		{
			return this.Data.MaterialCategories.All().Include(m => m.Materials);
		}

		public IQueryable<Material> GetAllMaterials()
		{
			throw new NotImplementedException();
		}

		public IQueryable<Material> GetAllMaterials(string materialCategorySlug)
		{
			return this.Data.Materials.All().Where(m => m.Category.Slug == materialCategorySlug);
		}

		public Material GetMaterial(int id)
		{
			throw new NotImplementedException();
		}

		public MaterialCategory GetMaterialCategory(int id)
		{
			return this.Data.MaterialCategories.Find(id);
		}

		public SurfaceMaterial GetSurfaceMaterial(int id)
		{
			throw new NotImplementedException();
		}

		public bool UpdateMaterial(int id, MaterialInputModel model)
		{
			throw new NotImplementedException();
		}

		public bool UpdateMaterial(int id, SurfaceMaterialInputModel model)
		{
			throw new NotImplementedException();
		}

		public MaterialCategory UpdateMaterialCategory(int id, EditMaterialCategoryInputModel model)
		{
			MaterialCategory dbCategory = this.Data.MaterialCategories.Find(id);
			if (dbCategory != null)
			{
				dbCategory = Mapper.Map(model, dbCategory);
				dbCategory.Slug = SlugGenerator.Generate(model.Name);
				dbCategory.LastModified = DateTime.UtcNow;
				this.Data.SaveChanges();
			}

			return dbCategory;
		}
	}
}
