using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MangaDownloader.Views
{
    /// <summary>
    /// Interaction logic for MangaManagerView.xaml
    /// </summary>
    public partial class MangaManagerView : Window
    {
        public MangaManagerView()
        {
            InitializeComponent();
        }

        private void loadingBlock_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Storyboard loadingFade = (Storyboard)TryFindResource("loadingBlink");

            if(loadingBlock.Visibility == Visibility.Collapsed)
            {
                loadingFade.Stop();
            }
            else
            {
                loadingFade.Begin();
            }
        }
    }
}
