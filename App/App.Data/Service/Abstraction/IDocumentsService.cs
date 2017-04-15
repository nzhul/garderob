using App.Models.Documents;

namespace App.Data.Service.Abstraction
{
	public interface IDocumentsService
	{
		Document GetDocument(int id);
	}
}