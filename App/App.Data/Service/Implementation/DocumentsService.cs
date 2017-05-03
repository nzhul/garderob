using System;
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

		public Document DeleteDocument(int id)
		{
			Document deletedDocument = this.Data.Documents.Delete(id);
			this.Data.SaveChanges();

			return deletedDocument;
		}

		public Document GetDocument(int id)
		{
			return this.Data.Documents.Find(id);
		}
	}
}
