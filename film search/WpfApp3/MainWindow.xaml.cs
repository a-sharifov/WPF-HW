using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApp3.Model;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void search_button_Click(object sender, RoutedEventArgs e)
        {
            string ResultSearch = await FilmSearchDownoloadService.FindFilm(search_box.Text);
            var titlesearch = DeserializeFilmSearchService.deserializeFilmInfo(ResultSearch);
            if (titlesearch != null)
            {
                title_name_text.Text = titlesearch.Country;
                ratings_title_box.Text = titlesearch.Ratings?[0].Value ?? "not found";
                plot_text.Text = titlesearch.Plot;
                actors_text.Text = titlesearch.Actors;
                genre_text.Text = titlesearch.Genre;
                if (titlesearch.Poster != null)
                {
                    using WebClient client = new WebClient();
                    var downoloadPicture = client.DownloadData(new Uri(titlesearch.Poster)); // возможная причина ошибки
                    //MemoryStream memoryStream = new MemoryStream(downoloadPicture);
                    //title_image.Source = ;
                }
            }
            search_box.Text = "";
        }
    }
}
