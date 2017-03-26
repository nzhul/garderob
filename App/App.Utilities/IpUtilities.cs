using System;
using System.Collections.Specialized;

namespace Utilities
{
	public static class IpUtilities
	{
		// serverVariables = HttpContext.Current.Request.ServerVariables
		public static string GetUserIp(NameValueCollection serverVariables)
		{
			if (serverVariables == null)
			{
				throw new ArgumentException("ServerVariables are required");
			}

			string userIp = serverVariables["HTTP_X_FORWARDED_FOR"];

			if (string.IsNullOrEmpty(userIp))
			{
				userIp = serverVariables["REMOTE_ADDR"];
			}

			return userIp;
		}
	}
}
