using System.Collections.Generic;

namespace App.Models.Materials
{
	public class LivePreviewViewModel
	{
		public IEnumerable<Material> SurfaceMaterials { get; set; }

		public IEnumerable<MaterialCategory> MaterialCategories { get; set; }
	}
}