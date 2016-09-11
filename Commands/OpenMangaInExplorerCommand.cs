using MangaDownloader.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MangaDownloader.Commands
{
    public class OpenMangaInExplorerCommand : ICommand
    {
        MangaManagerViewModel mangaManagerViewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public OpenMangaInExplorerCommand(MangaManagerViewModel viewModel)
        {
            this.mangaManagerViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            string mangaPath = parameter as string;

            if (Directory.Exists(Path.Combine(Properties.Settings.Default.MangaLibraryFolder, mangaPath)))
                return true;
            else
                return false;
        }

        public void Execute(object parameter)
        {
            string mangaPath = parameter as string;

            Process.Start(Path.Combine(Properties.Settings.Default.MangaLibraryFolder, mangaPath));
        }
    }
}
