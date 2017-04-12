using System.ComponentModel.DataAnnotations;

namespace App.Models.Images
{
	public class Image
	{
		[Key]
		public int Id { get; set; }

		public byte[] Big { get; set; }

		public byte[] Medium { get; set; }

		public byte[] Small { get; set; }
	}
}