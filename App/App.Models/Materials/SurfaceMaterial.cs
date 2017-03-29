using App.Models.Images;

namespace App.Models.Materials
{
	public class SurfaceMaterial : Material
	{
		public Image LivePreviewFrontImage { get; set; }

		public Image LivePreviewBackImage { get; set; }
	}
}
