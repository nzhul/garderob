using App.Data.Service.Abstraction;
using App.Models.Images;
using App.Models.Materials;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace App.Data.Service.Implementation
{
	public class MaterialsService : IMaterialsService
	{
		private IUoWData Data;

		public MaterialsService(IUoWData data)
		{
			this.Data = data;
		}

		public int CreateMaterial(EditMaterialInputModel model)
		{
			Material newMaterial = Mapper.Map(model, new Material());
			newMaterial.DateCreated = DateTime.UtcNow;
			newMaterial.LastModified = DateTime.UtcNow;

			string[] queries = this.GenerateQueries(model);

			byte[] smallImageData = Utilities.ImageUtilities.CropImage(model.PostedMaterialImage, queries[0]);
			byte[] mediumImageData = Utilities.ImageUtilities.CropImage(model.PostedMaterialImage, queries[1]);
			byte[] bigImageData = Utilities.ImageUtilities.CropImage(model.PostedMaterialImage, queries[2]);

			Image newImage = new Image
			{
				Small = smallImageData,
				Medium = mediumImageData,
				Big = bigImageData
			};

			this.Data.Images.Add(newImage);
			this.Data.SaveChanges();

			newMaterial.Image = newImage;

			this.Data.Materials.Add(newMaterial);
			this.Data.SaveChanges();

			return newMaterial.Id;
		}

		private string[] GenerateQueries(EditMaterialInputModel model)
		{
			string bigImageQueryTemplate = "width={0}&height={1}&crop=auto&format=jpg";
			string mediumImageQueryTemplate = "width={0}&height={1}&crop=auto&format=jpg";
			string smallImageQueryTemplate = "width={0}&height={1}&crop=auto&format=jpg";

			string[] smallSizeParts = model.SmallImageSize.Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
			string sWidth = smallSizeParts[0];
			string sHeight = smallSizeParts[1];

			string smallQuery = string.Format(smallImageQueryTemplate, sWidth, sHeight);

			string[] mediumSizeParts = model.MediumImageSize.Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
			string mWidth = mediumSizeParts[0];
			string mHeight = mediumSizeParts[1];

			string mediumQuery = string.Format(mediumImageQueryTemplate, mWidth, mHeight);

			string[] bigSizeParts = model.BigImageSize.Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
			string bWidth = bigSizeParts[0];
			string bHeight = bigSizeParts[1];

			string bigQuery = string.Format(bigImageQueryTemplate, bWidth, bHeight);

			return new string[3] { smallQuery, mediumQuery, bigQuery };
		}

		public int CreateMaterialCategory(EditMaterialCategoryInputModel model)
		{
			MaterialCategory newCategory = Mapper.Map(model, new MaterialCategory());
			newCategory.DateCreated = DateTime.UtcNow;
			newCategory.LastModified = DateTime.UtcNow;

			this.Data.MaterialCategories.Add(newCategory);
			this.Data.SaveChanges();

			return newCategory.Id;
		}

		public Material DeleteMaterial(int id)
		{
			Material materialToDelete = this.GetMaterial(id);

			if (materialToDelete != null)
			{
				this.Data.Images.Delete(materialToDelete.Image);
				this.Data.Materials.Delete(materialToDelete);
				this.Data.SaveChanges();
			}
			
			return materialToDelete;
		}

		public IQueryable<MaterialCategory> GetAllCategoriesWithMaterials()
		{
			return this.Data.MaterialCategories.All().Include(m => m.Materials);
		}

		public IQueryable<Material> GetAllMaterials()
		{
			throw new NotImplementedException();
		}

		public IQueryable<Material> GetAllMaterials(string materialCategorySlug)
		{
			return this.Data.Materials.All().Where(m => m.Category.Slug == materialCategorySlug);
		}

		public IEnumerable<SelectListItem> GetCategoriesSelectData()
		{
			var categories = this.Data.MaterialCategories
				.All()
				.OrderBy(x => x.Id)
				.Select(x => new SelectListItem
				{
					Value = x.Id.ToString(),
					Text = x.Name.ToString()
				});

			return new SelectList(categories, "Value", "Text");
		}

		public Material GetMaterial(int id)
		{
			return this.Data.Materials.Find(id);
		}

		public MaterialCategory GetMaterialCategory(int id)
		{
			return this.Data.MaterialCategories.Find(id);
		}

		public Material UpdateMaterial(int id, EditMaterialInputModel model)
		{
			Material dbMaterial = this.Data.Materials.Find(id);

			if (dbMaterial != null)
			{
				dbMaterial = Mapper.Map(model, dbMaterial);
				dbMaterial.LastModified = DateTime.UtcNow;

				if (model.PostedMaterialImage != null)
				{
					string[] queries = this.GenerateQueries(model);

					byte[] smallImageData = Utilities.ImageUtilities.CropImage(model.PostedMaterialImage, queries[0]);
					byte[] mediumImageData = Utilities.ImageUtilities.CropImage(model.PostedMaterialImage, queries[1]);
					byte[] bigImageData = Utilities.ImageUtilities.CropImage(model.PostedMaterialImage, queries[2]);

					Image newImage = new Image
					{
						Small = smallImageData,
						Medium = mediumImageData,
						Big = bigImageData
					};

					this.Data.Images.Delete(dbMaterial.Image);
					this.Data.Images.Add(newImage);
					this.Data.SaveChanges();

					dbMaterial.Image = newImage;
				}

				this.Data.SaveChanges();
			}

			return dbMaterial;
		}

		public MaterialCategory UpdateMaterialCategory(int id, EditMaterialCategoryInputModel model)
		{
			MaterialCategory dbCategory = this.Data.MaterialCategories.Find(id);
			if (dbCategory != null)
			{
				dbCategory = Mapper.Map(model, dbCategory);
				dbCategory.LastModified = DateTime.UtcNow;
				this.Data.SaveChanges();
			}

			return dbCategory;
		}
	}
}
