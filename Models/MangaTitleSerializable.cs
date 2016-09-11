using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader.Models
{
    [Serializable()]
    public class MangaTitleSerializable
    {
        public string MangaName { get; set; }
        public string MangaAddress { get; set; }
        public List<string> Chapters { get; set; }
        public int DownloadedChapters { get; set; }
        public string LastDownloadedPage { get; set; }
        public string MangaSource { get; set; }
    }
}
