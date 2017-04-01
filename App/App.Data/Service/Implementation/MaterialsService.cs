using System;
using System.Linq;
using App.Data.Service.Abstraction;
using App.Models.Materials;

namespace App.Data.Service.Implementation
{
	public class MaterialsService : IMaterialsService
	{
		private IUoWData data;

		public MaterialsService(IUoWData data)
		{
			this.data = data;
		}

		public int CreateMaterial(MaterialInputModel model)
		{
			throw new NotImplementedException();
		}

		public int CreateSurfaceMaterial(SurfaceMaterialInputModel model)
		{
			throw new NotImplementedException();
		}

		public bool DeleteMaterial(int id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Material> GetAllMaterials()
		{
			throw new NotImplementedException();
		}

		public IQueryable<Material> GetAllMaterials(string materialCategorySlug)
		{
			return this.data.Materials.All().Where(m => m.Category.Slug == materialCategorySlug);
		}

		public Material GetMaterial(int id)
		{
			throw new NotImplementedException();
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
	}
}
