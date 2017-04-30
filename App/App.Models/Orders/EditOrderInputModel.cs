using App.Models.Images;
using App.Models.Materials;
using App.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace App.Models.Orders
{
	public class EditOrderInputModel
	{
		public IList<Material> SurfaceMaterials { get; set; }

		public IList<Material> HandlesMaterials { get; set; }

		public ICollection<Image> SketchImages { get; set; }

		public ICollection<Image> DesignImages { get; set; }

		public ICollection<Image> ResultImages { get; set; }

		public List<HttpPostedFileBase> PostedSketches { get; set; }

		public List<HttpPostedFileBase> PostedDesigns { get; set; }

		public List<HttpPostedFileBase> PostedResults { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Име на проекта:")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = " *Невалидна дължина!")]
		public string Title { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Slug:")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = " *Невалидна дължина!")]
		public string Slug { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Допълнителна информация:")]
		[StringLength(2500, MinimumLength = 3, ErrorMessage = " *Невалидна дължина!")]
		[DataType(DataType.MultilineText)]
		public string OrderText { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Range(1, int.MaxValue, ErrorMessage = " * Задължително!")]
		public int BaseMaterialId { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Range(1, int.MaxValue, ErrorMessage = " * Задължително!")]
		public int DoorsMaterialId { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Range(1, int.MaxValue, ErrorMessage = " * Задължително!")]
		public int FazerMaterialId { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Range(1, int.MaxValue, ErrorMessage = " * Задължително!")]
		public int HandlesMaterialId { get; set; }

		[Display(Name = "Монтаж:")]
		public bool Installation { get; set; }

		[Display(Name = "Публична (Готови продукти):")]
		public bool IsPublic { get; set; }

		[Display(Name = "Брой:")]
		public int Count { get; set; }

		[Display(Name = "Цена:")]
		public decimal Price { get; set; }

		[Display(Name = "Статус:")]
		public OrderState State { get; set; }

		[Display(Name = "Дата на поръчката:")]
		public DateTime RequestDate { get; set; }

		public string ClientId { get; set; }

		public string ClientFullName { get; set; }

		[Display(Name = "Имена на клиента:")]
		public string AnonymousClientName { get; set; }

		[Display(Name = "Email на клиента:")]
		public string AnonymousClientEmail { get; set; }

		[Display(Name = "Телефон на клиента:")]
		public string AnonymousClientPhone { get; set; }

		public int OrderCategoryId { get; set; }

		[Required(ErrorMessage = "Задължително!")]
		[Display(Name = "Категория")]
		public int SelectedCategoryId { get; set; }
		public IEnumerable<SelectListItem> Categories { get; set; }
	}
}