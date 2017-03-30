using System.Web;

namespace App.Models.Materials
{
	public class SurfaceMaterialInputModel : MaterialInputModel
	{
		public HttpPostedFileBase FrontImage { get; set; }

		public HttpPostedFileBase BackImage { get; set; }

		public byte[] CurrentFrontImage { get; set; }

		public byte[] CurrentBackImage { get; set; }
	}
}