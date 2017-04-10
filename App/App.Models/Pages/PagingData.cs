using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Utilities;

namespace App.Models.Pages
{
	public class PagingData
	{
		public int TotalResultsCount { get; set; }

		public int PagesCount
		{
			get
			{
				return (int)Math.Ceiling((double)this.TotalResultsCount / (double)this.PageSize);
			}
		}

		public int PageSize { get; set; }

		public bool DisplayPrev
		{
			get
			{
				if (this.CurrentPage < 2)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		public string PrevUrl
		{
			get
			{
				return this.AddOrUpdatePageQueryParam(this.PageUrl, (this.CurrentPage - 1).ToString()).ToString();
			}
		}

		public string NextUrl
		{
			get
			{
				return this.AddOrUpdatePageQueryParam(this.PageUrl, (this.CurrentPage + 1).ToString()).ToString();
			}
		}

		public int CurrentPage
		{
			get
			{
				if (this.QueryString != null && !string.IsNullOrEmpty(this.QueryString["page"]))
				{
					int page = 1;
					int.TryParse(this.QueryString["page"], out page);
					return page;
				}
				else
				{
					return 1;
				}
			}
		}

		public bool DisplayNext
		{
			get
			{
				if (this.CurrentPage <= this.PagesCount - 1)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public int LinksRadius { get; set; }

		public bool UnstableResults { get; set; }

		public ICollection<PageUrlItem> Urls { get; set; }

		public Uri PageUrl { get; private set; }

		public NameValueCollection QueryString { get; set; }

		public int FromItem { get; set; }

		public int ToItem { get; set; }

		public PagingData(int resultsCount, int pageSize, int linksRadius, bool unstableResults, Uri pageUrl, NameValueCollection queryString)
		{
			this.PageUrl = pageUrl;
			this.QueryString = queryString;
			this.PageSize = pageSize;
			this.TotalResultsCount = resultsCount;
			this.LinksRadius = linksRadius;
			this.UnstableResults = unstableResults;
			this.Urls = this.GenerateUrls();
			this.FromItem = ((this.CurrentPage - 1) * this.PageSize) + 1;
			this.ToItem = this.CurrentPage * this.PageSize;
			if (this.ToItem > this.TotalResultsCount)
			{
				this.ToItem = this.TotalResultsCount;
			}
		}

		private ICollection<PageUrlItem> GenerateUrls()
		{
			ICollection<PageUrlItem> urls = new List<PageUrlItem>();
			int startIndex = this.GetPagingStartIndex();
			int endIndex = this.GetPagingEndIndex();

			if (startIndex > 1)
			{
				// Render the first item
				PageUrlItem urlItem = this.CreatePageUrlItem(1);
				urls.Add(urlItem);
			}

			if (startIndex > 2)
			{
				PageUrlItem dotsItem = this.CreateDotsItem();
				urls.Add(dotsItem);
			}

			for (int i = startIndex; i <= endIndex; i++)
			{
				PageUrlItem urlItem = this.CreatePageUrlItem(i);
				urls.Add(urlItem);
			}

			if (this.UnstableResults)
			{
				if (endIndex < this.PagesCount)
				{
					PageUrlItem dotsItem = this.CreateDotsItem();
					urls.Add(dotsItem);
				}
			}
			else
			{
				if (endIndex < this.PagesCount - 1)
				{
					PageUrlItem dotsItem = this.CreateDotsItem();
					urls.Add(dotsItem);
				}

				if (endIndex < this.PagesCount)
				{
					// Also render the last page item;
					PageUrlItem urlItem = this.CreatePageUrlItem(this.PagesCount);
					urls.Add(urlItem);
				}
			}

			return urls;
		}

		private PageUrlItem CreateDotsItem()
		{
			PageUrlItem item = new PageUrlItem();
			item.Url = string.Empty;
			item.Text = "...";

			return item;
		}

		private PageUrlItem CreatePageUrlItem(int i)
		{
			Uri pageUrl = this.PageUrl;
			pageUrl = this.AddOrUpdatePageQueryParam(pageUrl, i.ToString());

			PageUrlItem urlItem = new PageUrlItem();
			urlItem.Url = pageUrl.ToString();
			urlItem.IsSelected = i == this.CurrentPage ? true : false;
			urlItem.Text = i.ToString();

			return urlItem;
		}

		private int GetPagingStartIndex()
		{
			int startIndex = this.CurrentPage - this.LinksRadius;
			if (startIndex < 1)
			{
				startIndex = 1;
			}

			return startIndex;
		}

		private int GetPagingEndIndex()
		{
			int endIndex = this.CurrentPage + this.LinksRadius;
			if (endIndex > this.PagesCount)
			{
				endIndex = this.PagesCount;
			}

			return endIndex;
		}

		private Uri AddOrUpdatePageQueryParam(Uri pageUrl, string value)
		{
			if (string.IsNullOrEmpty(this.QueryString["page"]))
			{
				pageUrl = pageUrl.AddQueryParamValue("page", value);
			}
			else
			{
				pageUrl = pageUrl.UpdateQueryParamValue("page", value);
			}

			return pageUrl;
		}
	}
}