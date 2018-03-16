using App.Data.Service.Abstraction;
using App.Models.Images;
using System.Net.Mime;
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
                    case "medium":
                        imageData = dbImage.Medium;
                        break;
                    case "big":
                        imageData = dbImage.Big;
                        break;
                    default:
                        imageData = dbImage.Small;
                        break;
                }

                ContentDisposition cd = new ContentDisposition
                {
                    FileName = dbImage.Id + ".jpg",
                    Inline = true
                };

                Response.AppendHeader("Content-Disposition", cd.ToString());
                return File(imageData, "image/jpg");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}