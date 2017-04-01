using App.Models.Materials;
using System.Linq;

namespace App.Data.Service.Abstraction
{
	public interface IMaterialsService
	{
		int CreateMaterial(MaterialInputModel model);

		int CreateSurfaceMaterial(SurfaceMaterialInputModel model);

		bool DeleteMaterial(int id);

		bool UpdateMaterial(int id, MaterialInputModel model);

		bool UpdateMaterial(int id, SurfaceMaterialInputModel model);

		IQueryable<Material> GetAllMaterials();

		IQueryable<Material> GetAllMaterials(string materialCategorySlug);

		Material GetMaterial(int id);

		SurfaceMaterial GetSurfaceMaterial(int id);
	}
}