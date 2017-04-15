using App.Data.Service.Abstraction;
using App.Models.Documents;

namespace App.Data.Service.Implementation
{
	public class DocumentsService : IDocumentsService
	{
		private IUoWData Data;

		public DocumentsService(IUoWData data)
		{
			this.Data = data;
		}

		public Document GetDocument(int id)
		{
			return this.Data.Documents.Find(id);
		}
	}
}
