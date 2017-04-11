using App.Data.Service.Abstraction;
using App.Models.Images;

namespace App.Data.Service.Implementation
{
	public class ImagesService : IImagesService
	{
		IUoWData Data;

		public ImagesService(IUoWData data)
		{
			this.Data = data;
		}

		public Image GetImage(int id)
		{
			return this.Data.Images.Find(id);
		}
	}
}