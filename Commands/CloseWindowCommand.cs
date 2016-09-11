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
    public class CloseWindowCommand : ICommand
    {
        MangaManagerViewModel mangaManagerViewModel;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public CloseWindowCommand(MangaManagerViewModel viewModel)
        {
            mangaManagerViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            foreach(Thread thread in mangaManagerViewModel.DownloadingThreads)
            {
                thread.Abort();
            }
        }
    }
}
