using MangaDownloader.Models;
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
    public class AddMangaToLibraryCommand : ICommand
    {

        MangaManagerViewModel mangaManagerViewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AddMangaToLibraryCommand(MangaManagerViewModel viewModel)
        {
            this.mangaManagerViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            string selectedManga = parameter as string;

            var matches = mangaManagerViewModel.MangaListLibrary.Where(x => x.MangaName == selectedManga);

            if (matches.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Execute(object parameter)
        {
            string selectedMangaName = parameter as string;

            KeyValuePair<string, string> selectedManga = mangaManagerViewModel.MangaListSource.First(x => x.Value == selectedMangaName);

            MangaTitle mangaTitle = new MangaTitle();
            mangaTitle.MangaName = selectedManga.Value;
            mangaTitle.MangaAddress = selectedManga.Key;
            mangaTitle.MangaSource = mangaManagerViewModel.SelectedMangaSource;

            mangaTitle.Chapters = MangaFox.GetChapters(mangaTitle);
            mangaTitle.DownloadedChapters = 0;

            mangaManagerViewModel.MangaListLibrary.Add(mangaTitle);
            Helpers.UserSettings.SaveMangaTitles(mangaManagerViewModel.MangaListLibrary.ToList());
        }
    }
}
