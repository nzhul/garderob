using System.Collections.Generic;

namespace App.Models.Materials
{
	public class CalculatorViewModel
	{
		public IEnumerable<Material> SurfaceMaterials { get; set; }

		public IEnumerable<Material> FazerMaterials { get; set; }

		public IEnumerable<Material> HandleMaterials { get; set; }
	}
}