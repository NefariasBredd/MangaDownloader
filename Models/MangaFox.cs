using HtmlAgilityPack;
using MangaDownloader.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MangaDownloader.Models
{
    public static class MangaFox
    {
        public static string MangaList = "http://mangafox.me/manga/";

        public static Dictionary<string, string> Lista()
        {
            try
            {
                Dictionary<string, string> list = new Dictionary<string, string>();

                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load(MangaList);

                HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//*[starts-with(@class,'series_preview')]");

                foreach (HtmlNode node in nodes)
                {
                    list.Add(node.Attributes[0].Value.ToString(), node.InnerHtml.Replace("&quot;", "\""));
                }

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<string> GetChapters(MangaTitle mangaTitle)
        {
            List<string> mangaChapters = new List<string>();

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(mangaTitle.MangaAddress);

            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes(@"//a[@class=""tips""]");

            foreach (HtmlNode node in nodes)
            {
                mangaChapters.Add(node.Attributes[0].Value.ToString());
            }

            return mangaChapters;
        }

        public static void DownloadManga(MangaTitle mangaTitle, TrueObservableCollection<MangaTitle> MangaLibrary)
        {
            //return;

            string mainPath = Properties.Settings.Default.MangaLibraryFolder;

            //List<string> chapters = mangaTitle.Chapters;

            List<string> chapters = new List<string>(mangaTitle.Chapters);

            chapters.Reverse();

            bool savePointReached = string.IsNullOrEmpty(mangaTitle.LastDownloadedPage) ? true : false;

            foreach (string chapter in chapters)
            {
                string[] splittedURL = chapter.Split('/');
                string chapterNumber = splittedURL[splittedURL.Length - 2];
                string chapterPath = Path.Combine(mainPath, mangaTitle.MangaName, chapterNumber);
                string blankChapter = chapter.Substring(0, chapter.LastIndexOf('/'));

                //if (Directory.Exists(chapterPath))
                //    return;

                Directory.CreateDirectory(chapterPath);

                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = new HtmlDocument();

                int pageCounter = 0;
                string currentHTML = chapter;

                while (true)
                {
                    if (!string.IsNullOrEmpty(mangaTitle.LastDownloadedPage) && !savePointReached)
                    {
                        if (mangaTitle.LastDownloadedPage == currentHTML)
                            savePointReached = true;
                    }

                    if (!savePointReached)
                    {
                        continue;
                    }

                    var data = new MyWebClient().DownloadString(currentHTML);
                    document.LoadHtml(data);

                    HtmlNode imgNode = document.DocumentNode.SelectSingleNode(@"//img[@id='image'][1]");
                    HtmlNode divNode = imgNode.ParentNode;

                    string imgURL = imgNode.Attributes.First(m => m.Name == "src").Value;

                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(imgURL, Path.Combine(chapterPath, pageCounter.ToString() + ".jpg"));
                    }

                    string hrefURL = divNode.Attributes.First(m => m.Name == "href").Value;

                    if (hrefURL.Trim() == "javascript:void(0);")
                    {
                        Application.Current.Dispatcher.Invoke((Action)(() =>
                        {
                            mangaTitle.DownloadedChapters++;
                            mangaTitle.LastDownloadedPage = currentHTML;
                        }));

                        UserSettings.SaveMangaTitles(MangaLibrary.ToList());
                        break;
                    }
                    else
                    {
                        pageCounter++;
                        currentHTML = blankChapter + "/" + hrefURL.Trim();

                        Application.Current.Dispatcher.Invoke((Action)(() => 
                        {
                            mangaTitle.LastDownloadedPage = currentHTML;
                        }));

                        UserSettings.SaveMangaTitles(MangaLibrary.ToList());
                    }
                }
            }
        }
    }

    class MyWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            //return base.GetWebRequest(address);

            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }
    }
}
