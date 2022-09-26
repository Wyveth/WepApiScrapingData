using HtmlAgilityPack;
using ScrapySharp.Network;
using System.Net;

namespace WepApiScrapingData.Utils
{
    public static class HttpClientUtils
    {
        #region Call Url Static/Dynamic
        public static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            var response = await client.GetAsync(fullUrl);
            return await response.Content.ReadAsStringAsync();
        }

        public static HtmlNode CallUrlDynamic(string fullUrl)
        {
            ScrapingBrowser _scrapBrowser = new ScrapingBrowser() { IgnoreCookies = true, Timeout = TimeSpan.FromMinutes(5), };
            _scrapBrowser.Headers["Accept"] = "text/html, application/xhtml+xml, */*";
            _scrapBrowser.Headers["User-Agent"] = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW 64; Trident/5.0)";
            return _scrapBrowser.NavigateToPage(new Uri(fullUrl)).Html;
        }
        #endregion

        #region Download File
        public static async Task DownloadFileTaskAsync(this HttpClient client, Uri uri, string FileName)
        {
            using (var s = await client.GetStreamAsync(uri))
            {
                using (var fs = new FileStream(FileName, FileMode.CreateNew))
                {
                    await s.CopyToAsync(fs);
                }
            }
        }
        #endregion
    }
}
