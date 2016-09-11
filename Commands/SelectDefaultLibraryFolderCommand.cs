using MangaDownloader.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MangaDownloader.Commands
{
    class SelectDefaultLibraryFolderCommand : ICommand
    {
        MangaManagerViewModel mangaManagerViewModel;

        public SelectDefaultLibraryFolderCommand(MangaManagerViewModel viewModel)
        {
            this.mangaManagerViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                string file = fbd.SelectedPath;

                mangaManagerViewModel.MangaLibraryFolder = file;
            }
        }
    }
}
