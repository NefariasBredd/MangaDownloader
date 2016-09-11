using MangaDownloader.Helpers;
using MangaDownloader.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MangaDownloader.ViewModels
{
    public class MangaManagerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> MangaSources { get; private set; }

        private string _SelectedMangaSource;
        public string SelectedMangaSource
        {
            get { return _SelectedMangaSource; }
            set { _SelectedMangaSource = value; DownloadAllMangaNames(); }
        }

        private string _FilterMangaName;
        public string FilterMangaName
        {
            get { return _FilterMangaName; }
            set { _FilterMangaName = value; FilterMangaByName(); }
        }

        private Visibility _MangaNameVisibility;
        public Visibility MangaNameVisibility
        {
            get { return _MangaNameVisibility; }
            set { _MangaNameVisibility = value; NotifyPropertyChanged("MangaNameVisibility"); }
        }

        private Visibility _MangaListVisibility;
        public Visibility MangaListVisibility
        {
            get { return _MangaListVisibility; }
            set { _MangaListVisibility = value; NotifyPropertyChanged("MangaListVisibility"); }
        }

        private Visibility _LoadMangaListVisibility;
        public Visibility LoadMangaListVisibility
        {
            get { return _LoadMangaListVisibility; }
            set { _LoadMangaListVisibility = value; NotifyPropertyChanged("LoadMangaListVisibility"); }
        }

        private Dictionary<string, string> mangaWholeList;

        private Dictionary<string, string> _MangaListSource;
        public Dictionary<string, string> MangaListSource
        {
            get { return _MangaListSource; }
            set { _MangaListSource = value; NotifyPropertyChanged("MangaListSource"); }
        }

        private string _SelectedMangaName;
        public string SelectedMangaName
        {
            get { return _SelectedMangaName; }
            set { _SelectedMangaName = value; NotifyPropertyChanged("SelectedMangaName"); }
        }

        private TrueObservableCollection<MangaTitle> _MangaListLibrary;
        public TrueObservableCollection<MangaTitle> MangaListLibrary
        {
            get { return _MangaListLibrary; }
            set { _MangaListLibrary = value; NotifyPropertyChanged("MangaLibraryList"); }
        }

        public List<Thread> _DownloadingThreads;
        public List<Thread> DownloadingThreads
        {
            get
            {
                if (_DownloadingThreads == null)
                {
                    _DownloadingThreads = new List<Thread>();
                }
                return _DownloadingThreads;
            }
            set { _DownloadingThreads = value; }
        }

        public ICommand AddMangaToLibraryCommand
        {
            get;
            private set;
        }
        public ICommand SelectDefaultLibraryFolderCommand
        {
            get;
            private set;
        }
        public ICommand DeleteMangaFromLibrary
        {
            get;
            private set;
        }
        public ICommand OpenMangaInExplorerCommand
        {
            get;
            private set;
        }
        public ICommand DownloadMangaCommand
        {
            get;
            private set;
        }
        public ICommand CloseWindowCommand
        {
            get;
            private set;
        }

        private string _MangaLibraryFolder;
        public string MangaLibraryFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_MangaLibraryFolder))
                {
                    _MangaLibraryFolder = Properties.Settings.Default.MangaLibraryFolder;
                }

                return _MangaLibraryFolder;
            }

            set
            {
                _MangaLibraryFolder = value;
                Properties.Settings.Default.MangaLibraryFolder = _MangaLibraryFolder;
                Properties.Settings.Default.Save();
                NotifyPropertyChanged("MangaLibraryFolder");
            }
        }

        public MangaManagerViewModel()
        {
            MangaSources = new ObservableCollection<string>();
            MangaSources.Add("mangafox.me");
            MangaSources.Add("mangahere.co");
            MangaSources.Add("mangastream.com");

            MangaNameVisibility = Visibility.Collapsed;
            MangaListVisibility = Visibility.Collapsed;
            LoadMangaListVisibility = Visibility.Collapsed;

            MangaListLibrary = new TrueObservableCollection<MangaTitle>(Helpers.UserSettings.LoadMangaTitles());

            AddMangaToLibraryCommand = new Commands.AddMangaToLibraryCommand(this);
            SelectDefaultLibraryFolderCommand = new Commands.SelectDefaultLibraryFolderCommand(this);
            DeleteMangaFromLibrary = new Commands.DeleteMangaFromLibrary(this);
            OpenMangaInExplorerCommand = new Commands.OpenMangaInExplorerCommand(this);
            DownloadMangaCommand = new Commands.DownloadMangaCommand(this);
            CloseWindowCommand = new Commands.CloseWindowCommand(this);
            //Properties.Settings.Default.MangaTitles = null;
            //Properties.Settings.Default.Save();
        }

        private void DownloadAllMangaNames()
        {
            LoadMangaListVisibility = Visibility.Visible;

            Thread deeperInRabbitHole = new Thread(() =>
            {
                MangaListSource = MangaFox.Lista();

                if (MangaListSource != null)
                {
                    mangaWholeList = new Dictionary<string, string>(MangaListSource);
                    LoadMangaListVisibility = Visibility.Collapsed;
                    MangaNameVisibility = Visibility.Visible;
                    MangaListVisibility = Visibility.Visible;
                }
            });

            deeperInRabbitHole.Start();
        }



        private void FilterMangaByName()
        {
            MangaListSource = new Dictionary<string, string>(mangaWholeList.Where(x => x.Value.ToLower().Contains(FilterMangaName.ToLower())).ToDictionary(x => x.Key, x => x.Value));
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
