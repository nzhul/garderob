using System;
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

		public bool DeleteImage(int id)
		{
			Image deletedImage = this.Data.Images.Delete(id);
			this.Data.SaveChanges();

			if (deletedImage != null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public Image GetImage(int id)
		{
			return this.Data.Images.Find(id);
		}
	}
}