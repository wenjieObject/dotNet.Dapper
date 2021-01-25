using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Integration
{
    public class RequestUtil
    {
		public static string HttpPost(string url, string postData = null, string contentType = null, Dictionary<string, string> headerDic = null, int timeOut = 30)
		{
			if (string.IsNullOrEmpty(postData))
			{
				postData = "";
			}
			string result;
			using (HttpClient httpClient = new HttpClient())
			{
				httpClient.Timeout = new TimeSpan(0, 0, timeOut);
				using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
				{
					if (contentType != null)
					{
						httpContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
					}
					if (headerDic != null)
					{
						foreach (KeyValuePair<string, string> keyValuePair in headerDic)
						{
							httpContent.Headers.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					result = httpClient.PostAsync(url, httpContent).Result.Content.ReadAsStringAsync().Result;
				}
			}
			return result;
		}
	}
}
