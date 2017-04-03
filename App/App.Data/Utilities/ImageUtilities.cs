using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.Data.Utilities
{
	public static class ImageUtilities
	{
		/// <summary>
		/// Crop the image with ImageResizer Library and return byte[]
		/// </summary>
		/// <param name="uploadedImage">HttpPostedFileBase comming directly from the input Model</param>
		/// <param name="cropQuery">Ex: "width=150&height=150&crop=auto&format=jpg"</param>
		/// <returns></returns>
		public static byte[] CropImage(HttpPostedFileBase uploadedImage, string cropQuery)
		{
			MemoryStream stream = new MemoryStream();

			ImageResizer.ImageJob i = new ImageResizer.ImageJob(uploadedImage, stream, new ImageResizer.Instructions(cropQuery));
			i.Build();

			return stream.ToArray();
		}
	}
}
