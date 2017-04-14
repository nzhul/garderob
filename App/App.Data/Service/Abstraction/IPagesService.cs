using App.Models.Pages;
using App.Models.ViewModels;
using System.Collections.Generic;

namespace App.Data.Service.Abstraction
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

		PageViewModel GetPageBySlug(string slug);

		PageViewModel GetFeaturedCustomPage(int pageId);
	}
}
