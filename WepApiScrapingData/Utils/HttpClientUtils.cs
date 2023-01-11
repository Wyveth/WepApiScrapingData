using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using ScrapySharp.Network;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;

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

        public static async Task<string> CallUrlNoRedirect(string fullUrl)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.AllowAutoRedirect = false;
            HttpClient client = new HttpClient(httpClientHandler);
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

        public async static Task<string> CallUrlDynamicTest(string fullUrl)
        {
            ScrapingBrowser _scrapBrowser = new ScrapingBrowser() { IgnoreCookies = true, Timeout = TimeSpan.FromMinutes(5) };
            _scrapBrowser.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            _scrapBrowser.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36";
            return await _scrapBrowser.DownloadStringAsync(new Uri(fullUrl));

        }
        #endregion

        #region Download File
        public static async Task<string> DownloadFileTaskAsync(this HttpClient client, string uri, string FileName, int Generation, bool Sprite = false)
        {
            string path = "Content/Images/G" + Generation;
            if (Sprite)
                path = "Content/Sprites/G" + Generation;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, FileName + ".png");

            if (!File.Exists(path))
            {
                using (var response = await client.GetAsync(uri))
                {
                    Console.WriteLine(FileName);
                    response.EnsureSuccessStatusCode();
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                    }
                }
            }

            return path;
        }

        public static async Task<string> DownloadFileTaskAsync(this HttpClient client, string uri, string FileName, string typeFile)
        {
            string path = "";

            switch (typeFile)
            {
                case Constantes.MiniGo:
                    path = "Content/Types/MiniGo/";
                    break;
                case Constantes.FondGo:
                    path = "Content/Types/FondGo/";
                    break;
                case Constantes.MiniHome:
                    path = "Content/Types/MiniHome/FR/";
                    break;
                case Constantes.IconHome:
                    path = "Content/Types/IconHome/";
                    break;
                case Constantes.AutoHome:
                    path = "Content/Types/AutoHome/";
                    break;
                default:
                    break;
            }

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, FileName + ".png");

            if (!File.Exists(path))
            {
                using (var response = await client.GetAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                    }
                }
            }

            return path;
        }

        public static async Task<string> DownloadTypeAttackFileTaskAsync(this HttpClient client, string uri, string FileName)
        {
            string path = "Content/TypeAttaque";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, FileName + ".png");

            if (!File.Exists(path))
            {
                using (var response = await client.GetAsync(uri))
                {
                    Console.WriteLine(FileName);
                    response.EnsureSuccessStatusCode();
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                    }
                }
            }

            return path;
        }

        public static async Task<string> DownloadSoundFileTaskAsync(this HttpClient client, string uri, string FileName, int Generation)
        {
            string path = "Content/Sound/G" + Generation;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, FileName + ".ogg");

            if (!File.Exists(path) && !string.IsNullOrEmpty(uri))
            {
                using (var response = await client.GetAsync(uri))
                {
                    response.EnsureSuccessStatusCode();
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                        {
                            await stream.CopyToAsync(fileStream);
                        }
                    }
                }
            }

            return path;
        }

        public static async Task<byte[]?> DownloadImageAsync(this HttpClient client, string url)
        {
            try
            {
                using (var httpResponse = await client.GetAsync(url))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return await httpResponse.Content.ReadAsByteArrayAsync();
                    }

                    return null;
                }
            }
            catch (Exception e)
            {
                //Handle Exception
                if (e == null)
                    throw new ArgumentNullException(e.Message);

                return null;
            }
        }
        #endregion
    }
}
