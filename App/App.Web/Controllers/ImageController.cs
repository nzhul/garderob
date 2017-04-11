using App.Data.Service.Abstraction;
using App.Models.Images;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class ImageController : Controller
	{
		IImagesService imagesService;

		public ImageController(IImagesService imagesService)
		{
			this.imagesService = imagesService;
		}

		public ActionResult Show(int id, string imageType)
		{
			Image dbImage = this.imagesService.GetImage(id);

			if (dbImage != null)
			{
				byte[] imageData = null;

				switch (imageType)
				{
					case "small":
						imageData = dbImage.Small;
						break;
					case "big":
						imageData = dbImage.Big;
						break;
					default:
						imageData = dbImage.Small;
						break;
				}

				//TODO: return png by default or by setting
				return File(imageData, "image/jpg");
			}
			else
			{
				return HttpNotFound();
			}
		}
	}
}