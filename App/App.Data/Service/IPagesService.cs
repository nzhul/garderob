using App.Models.InputModels;
using App.Models.ViewModels;
using System.Collections.Generic;

namespace App.Data.Service
{
	public interface IPagesService
	{
		IEnumerable<PageViewModel> GetPages();

		PageViewModel GetPageById(int id);

		int CreatePage(CreatePageInputModel inputModel);

		bool PageExists(int id);

		CreatePageInputModel GetPageInputModelById(int id);

		bool UpdatePage(int id, CreatePageInputModel inputModel);

		bool DeletePage(int id);

		PageViewModel GetPageByUrlName(string urlName);

		PageViewModel GetFeaturedCustomPage(int pageId);
	}
}
