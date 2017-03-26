using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities
{
	public static class UrlUtilities
	{
		private static string GetCurrentRequestUrl()
		{
			return HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped);
		}

		public static string RemoveExtension(string path)
		{
			if (path == null)
			{
				return null;
			}

			if (!Path.HasExtension(path) && !path.EndsWith("."))
			{
				return path;
			}

			int extensionIndex = path.LastIndexOf(".");
			if (extensionIndex >= 0)
			{
				int prevChar = extensionIndex - 1;
				if (prevChar >= 0 && path[prevChar] == '/')
				{
					return path;
				}

				path = path.Substring(0, extensionIndex);
			}

			return path;
		}

		public static string AddQueryParam(this Uri uri, string key, string value)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException("Key argument is null or empty");
			}

			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException("Value argument is null or empty");
			}

			return uri.AddQueryParams(new Dictionary<string, string>
			{
				{ key, value },
			});
		}

		public static string AddQueryParams(this Uri uri, IDictionary<string, string> parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentException("Parameters argument is null or empty");
			}

			UriBuilder uriBuilder = new UriBuilder(uri).HandleDefaultPort();
			NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);

			foreach (var kvp in parameters)
			{
				query[kvp.Key] = parameters[kvp.Key];
			}

			uriBuilder.Query = query.ToString();

			return uriBuilder.ToString();
		}

		public static Uri GetCurrentUrl(this HttpRequest request)
		{
			return new Uri(request.Url, request.RawUrl);
		}

		public static Uri GetCurrentUrl(this HttpRequestBase request)
		{
			return new Uri(request.Url, request.RawUrl);
		}

		private static UriBuilder HandleDefaultPort(this UriBuilder uriBuilder)
		{
			if (uriBuilder.Port == 80 || uriBuilder.Port == 443)
			{
				uriBuilder.Port = -1;
			}

			return uriBuilder;
		}

		public static Uri GetUriFromUrlPath(this string urlPath, HttpContext context)
		{
			return urlPath.GetUriFromUrlPath(new HttpContextWrapper(context));
		}

		public static Uri GetUriFromUrlPath(this string urlPath, HttpContextBase context)
		{
			string schemeAndServerUrl = context.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);

			return new Uri(string.Concat(schemeAndServerUrl, urlPath));
		}

		public static string GetHostAndPath(this Uri url)
		{
			string urlHostAndPath = url.GetComponents(UriComponents.SchemeAndServer | UriComponents.Path, UriFormat.Unescaped);

			return urlHostAndPath;
		}

		public static bool AreEqual(Uri firstUrl, Uri secondUrl)
		{
			if (firstUrl == null)
			{
				throw new ArgumentException("firstUrl argument is null or empty");
			}

			if (secondUrl == null)
			{
				throw new ArgumentException("secondUrl argument is null or empty");
			}

			string firstUrlString = firstUrl.GetComponents(UriComponents.Host | UriComponents.Path, UriFormat.Unescaped);
			string secondUrlString = secondUrl.GetComponents(UriComponents.Host | UriComponents.Path, UriFormat.Unescaped);

			return firstUrlString.Equals(secondUrlString, StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>
		/// Ensures that the provided url is returned with proper protocol. The proper protocol is deducted from 
		/// the current HttpContext request url. If there is no current httpContext, http is the default. If protocol
		/// exists in @url, the same value is returned
		/// </summary>
		/// <param name="url">The URL. If path is provided - exception is thrown.</param>
		/// <returns></returns>
		public static string EnsureProtocol(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				throw new ArgumentException("url parameter is null or empty");
			}

			if (url.StartsWith("/") && !url.StartsWith("//"))
			{
				throw new ArgumentException("Url should not be path", "url");
			}

			if (url.StartsWith("http://") || url.StartsWith("https://"))
			{
				return url;
			}
			else
			{
				string urlWithProtocol;
				urlWithProtocol = url.TrimStart(new char[] { '/' });

				if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Url != null)
				{
					urlWithProtocol = string.Format("{0}://{1}", HttpContext.Current.Request.Url.Scheme, urlWithProtocol);
				}
				else
				{
					urlWithProtocol = string.Format("http://{0}", urlWithProtocol);
				}

				return urlWithProtocol;
			}
		}

		public static Uri RemoveQueryStringByKeyAndValue(this Uri uri, string key, string value)
		{
			NameValueCollection newQueryString = HttpUtility.ParseQueryString(uri.Query);

			string[] queryParameterValues = new string[0];

			if (!string.IsNullOrEmpty(value))
			{
				queryParameterValues = newQueryString[key].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

				int indexToRemove = Array.IndexOf(queryParameterValues, value);

				List<string> tempCollecton = new List<string>(queryParameterValues);
				tempCollecton.RemoveAt(indexToRemove);

				queryParameterValues = tempCollecton.ToArray();
			}

			if (queryParameterValues.Length == 0)
			{
				newQueryString.Remove(key);
			}
			else
			{
				newQueryString.Remove(key);
				for (int i = 0; i < queryParameterValues.Length; i++)
				{
					newQueryString.Add(key, queryParameterValues[i]);
				}
			}

			string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

			string resultUrlString;

			if (newQueryString.Count > 0)
			{
				resultUrlString = string.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString);
			}
			else
			{
				resultUrlString = pagePathWithoutQueryString;
			}

			return new Uri(resultUrlString);
		}

		public static Uri RemoveQueryStringByKey(this Uri uri, string key)
		{
			return uri.RemoveQueryStringByKeyAndValue(key, null);
		}

		public static Uri AddQueryParamValue(this Uri uri, string key, string value)
		{
			NameValueCollection newQueryString = HttpUtility.ParseQueryString(uri.Query);
			string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

			newQueryString.Add(key, value);

			string resultUrlString = string.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString);

			return new Uri(resultUrlString);
		}

		public static Uri UpdateQueryParamValue(this Uri uri, string key, string value)
		{
			NameValueCollection newQueryString = HttpUtility.ParseQueryString(uri.Query);
			string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

			newQueryString.Set(key, value);

			string resultUrlString = string.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString);

			return new Uri(resultUrlString);
		}
	}
}
