using System;
using System.Linq;
using App.Data.Service.Abstraction;
using App.Models.Materials;

namespace App.Data.Service.Implementation
{
	public class MaterialsService : IMaterialsService
	{
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

		public IQueryable<SurfaceMaterial> GetAllSurfaceMaterials()
		{
			throw new NotImplementedException();
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
