using MangaDownloader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MangaDownloader.Commands
{
    public class DeleteMangaFromLibrary : ICommand
    {
        MangaManagerViewModel mangaManagerViewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DeleteMangaFromLibrary(MangaManagerViewModel viewModel)
        {
            this.mangaManagerViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string mangaName = parameter as string;

            mangaManagerViewModel.MangaListLibrary.Remove(mangaManagerViewModel.MangaListLibrary.First(m => m.MangaName == mangaName));

            Helpers.UserSettings.SaveMangaTitles(mangaManagerViewModel.MangaListLibrary.ToList());
        }
    }
}
