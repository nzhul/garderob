using App.Models.Images;

namespace App.Models.Materials
{
	public class SurfaceMaterial : Material
	{
		public virtual Image LivePreviewFrontImage { get; set; }

		public virtual Image LivePreviewBackImage { get; set; }
	}
}
