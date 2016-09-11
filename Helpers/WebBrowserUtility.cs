using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MangaDownloader.Helpers
{
    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserUtility), 
                new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;

            if(browser != null)
            {
                browser = new WebBrowser();
                uri = e.NewValue as string;

                browser.LoadCompleted += Browser_LoadCompleted;

                browser.Navigate(uri);
            }
        }

        private static void Browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;

            mshtml.HTMLDocument document = browser.Document as HTMLDocument;

            if (document != null)
            {
                string source = document.documentElement.innerHTML;
                MessageBox.Show(source);
            }
        }

        private static string uri;
    }
}
