using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader.Models
{
    [Serializable()]
    public class MangaTitle : INotifyPropertyChanged
    {
        private string _MangaName;
        public string MangaName
        {
            get { return _MangaName; }
            set { _MangaName = value; NotifyPropertyChanged("MangaName"); }
        }

        protected string _MangaAddress;
        public string MangaAddress
        {
            get { return _MangaAddress; }
            set { _MangaAddress = value; NotifyPropertyChanged("MangaAddress"); }
        }

        protected List<string> _Chapters;
        public List<string> Chapters
        {
            get { return _Chapters; }
            set { _Chapters = value; NotifyPropertyChanged("Chapters"); }
        }

        protected int _DownloadedChapters;
        public int DownloadedChapters
        {
            get { return _DownloadedChapters; }
            set { _DownloadedChapters = value; NotifyPropertyChanged("DownloadedChapters"); }
        }

        protected string _LastDownloadedPage;

        public string LastDownloadedPage
        {
            get { return _LastDownloadedPage; }
            set { _LastDownloadedPage = value; NotifyPropertyChanged("LastDownloadedPage"); }
        }

        protected string _MangaSource;
        public string MangaSource
        {
            get { return _MangaSource; }
            set { _MangaSource = value; NotifyPropertyChanged("MangaSource"); }
        }

        public MangaTitle()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
