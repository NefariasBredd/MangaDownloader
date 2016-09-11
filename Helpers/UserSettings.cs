using MangaDownloader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader.Helpers
{
    public static class UserSettings
    {
        public static void SaveMangaTitles(List<MangaTitle> mangaTitles)
        {
            List<MangaTitleSerializable> titlesSerializable = new List<MangaTitleSerializable>();

            foreach(MangaTitle title in mangaTitles)
            {
                MangaTitleSerializable titleSerializable = new MangaTitleSerializable();
                titleSerializable.Chapters = title.Chapters;
                titleSerializable.DownloadedChapters = title.DownloadedChapters;
                titleSerializable.LastDownloadedPage = title.LastDownloadedPage;
                titleSerializable.MangaAddress = title.MangaAddress;
                titleSerializable.MangaName = title.MangaName;
                titleSerializable.MangaSource = title.MangaSource;
                titlesSerializable.Add(titleSerializable);
            }

            using(MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, titlesSerializable);
                ms.Position = 0;
                byte[] buffer = new byte[(int)ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                Properties.Settings.Default.MangaTitles = Convert.ToBase64String(buffer);
                Properties.Settings.Default.Save();
            }
        }

        public static List<MangaTitle> LoadMangaTitles()
        {
            //SaveMangaTitles(new List<MangaTitle>());

            if (string.IsNullOrEmpty(Properties.Settings.Default.MangaTitles))
                return new List<MangaTitle>();

            List<MangaTitle> titlesSerializable = new List<MangaTitle>();

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(Properties.Settings.Default.MangaTitles)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                //return (List<MangaTitle>)bf.Deserialize(ms);

                List<MangaTitleSerializable> serializable = (List<MangaTitleSerializable>)bf.Deserialize(ms);
                List<MangaTitle> titles = new List<MangaTitle>();

                foreach(MangaTitleSerializable serialized in serializable)
                {
                    MangaTitle title = new MangaTitle();
                    title.Chapters = serialized.Chapters;
                    title.DownloadedChapters = serialized.DownloadedChapters;
                    title.LastDownloadedPage = serialized.LastDownloadedPage;
                    title.MangaAddress = serialized.MangaAddress;
                    title.MangaName = serialized.MangaName;
                    title.MangaSource = serialized.MangaSource;
                    titles.Add(title);
                }

                return titles;
            }            
        }
    }
}
