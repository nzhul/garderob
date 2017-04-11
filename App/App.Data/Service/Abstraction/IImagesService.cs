using App.Models.Images;

namespace App.Data.Service.Abstraction
{
	public interface IImagesService
	{
		Image GetImage(int id);
	}
}