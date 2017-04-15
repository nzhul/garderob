using System.ComponentModel.DataAnnotations;

namespace App.Models.Documents
{
	public class Document
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public byte[] Data { get; set; }

		public DocumentType Type { get; set; }
	}
}