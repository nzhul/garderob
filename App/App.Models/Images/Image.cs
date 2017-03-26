using System.ComponentModel.DataAnnotations;

namespace App.Models.Images
{
	public class Image
	{
		[Key]
		public int Id { get; set; }

		public byte[] ImageData { get; set; }
	}
}
