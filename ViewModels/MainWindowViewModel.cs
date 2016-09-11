using MangaDownloader.Models;
using mshtml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MangaDownloader.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> MangaSources { get; private set; }

        private string _SelectedSource;
        public string SelectedSource
        {
            get { return _SelectedSource; }
            set { _SelectedSource = value; GetAllMangaNames(); }
        }

        private string _FilterMangaName;

        public string FilterMangaName
        {
            get { return _FilterMangaName; }
            set { _FilterMangaName = value; }
        }

        private string _WebAddress;
        public string WebAddress
        {
            get { return _WebAddress; }
            set { _WebAddress = value; NotifyPropertyChanged("WebAddress"); }
        }

        private Visibility _MangaNameVisibility;
        public Visibility MangaNameVisibility
        {
            get { return _MangaNameVisibility; }
            set { _MangaNameVisibility = value; NotifyPropertyChanged("MangaNameVisibility"); }
        }

        private Visibility _LoadingMangaListVisibility;
        public Visibility LoadingMangaListVisibility
        {
            get { return _LoadingMangaListVisibility; }
            set { _LoadingMangaListVisibility = value;  NotifyPropertyChanged("LoadingMangaListVisibility"); }
        }

        private Visibility _MangaListViewVisibility;
        public Visibility MangaListViewVisibility
        {
            get { return _MangaListViewVisibility; }
            set { _MangaListViewVisibility = value; NotifyPropertyChanged("MangaListViewVisibility"); }
        }

        private Dictionary<string, string> _MangaList;
        public Dictionary<string, string> MangaList
        {
            get { return _MangaList; }
            set { _MangaList = value; NotifyPropertyChanged("MangaList"); }
        }

        private KeyValuePair<string, string> _MangaListSelectedItem;
        public KeyValuePair<string, string> MangaListSelectedItem
        {
            get { return _MangaListSelectedItem; }
            set { _MangaListSelectedItem = value; NotifyPropertyChanged("MangaListSelectedItem"); }
        }

        public MainWindowViewModel()
        {
            MangaSources = new ObservableCollection<string>();
            MangaSources.Add("mangafox.me");


            MangaNameVisibility = Visibility.Collapsed;
            LoadingMangaListVisibility = Visibility.Collapsed;
            MangaListViewVisibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void GetAllMangaNames()
        {
            LoadingMangaListVisibility = Visibility.Visible;

            WebAddress = string.Format("http://{0}", SelectedSource);

            Thread mangaListDownloading = new Thread(() => 
            {
                MangaList = MangaFox.Lista();
                LoadingMangaListVisibility = Visibility.Collapsed;
                MangaListViewVisibility = Visibility.Visible;
                
            });

            mangaListDownloading.Start();
        }
    }
}
