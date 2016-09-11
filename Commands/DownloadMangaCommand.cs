using MangaDownloader.Models;
using MangaDownloader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MangaDownloader.Commands
{
    class DownloadMangaCommand : ICommand
    {
        MangaManagerViewModel mangaManagerViewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DownloadMangaCommand(MangaManagerViewModel viewModel)
        {
            this.mangaManagerViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            string mangaName = parameter as string;

            if (mangaManagerViewModel.MangaListLibrary.Count(m => m.MangaName == mangaName) > 0)
            {
                MangaTitle mangaTitle = mangaManagerViewModel.MangaListLibrary.First(m => m.MangaName == mangaName);

                if (mangaTitle.DownloadedChapters < mangaTitle.Chapters.Count)
                    return true;
            }

            return false;            
        }

        public void Execute(object parameter)
        {
            Thread thread = new Thread(() => 
            {
                string mangaName = parameter as string;

                MangaTitle mangaTitle = mangaManagerViewModel.MangaListLibrary.First(m => m.MangaName == mangaName);

                MangaFox.DownloadManga(mangaTitle, mangaManagerViewModel.MangaListLibrary);
            });


            mangaManagerViewModel.DownloadingThreads.Add(thread);
            thread.Start();
        }
    }
}
