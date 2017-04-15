using App.Data.Service.Abstraction;
using App.Models.Documents;
using System.Web.Mvc;

namespace App.Web.Controllers
{
	public class DocumentController : Controller
	{
		IDocumentsService documentsService;

		public DocumentController(IDocumentsService documentsService)
		{
			this.documentsService = documentsService;
		}

		public ActionResult Show(int id)
		{
			Document dbDocument = this.documentsService.GetDocument(id);

			if (dbDocument != null)
			{
				string documentType = string.Empty;

				switch (dbDocument.Type)
				{
					case DocumentType.PDF:
						documentType = "application/pdf";
						break;
					default:
						break;
				}

				byte[] documentData = dbDocument.Data;

				return File(documentData, documentType);
			}
			else
			{
				return HttpNotFound();
			}
		}
	}
}