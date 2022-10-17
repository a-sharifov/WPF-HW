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

        private void search_button_Click(object sender, RoutedEventArgs e)
        {
            Search_Title();
        }

        private void search_box_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                Search_Title();
        }

        private async void Search_Title()
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
                if (titlesearch.Poster != null) title_image.Source = new BitmapImage(new Uri(titlesearch.Poster));
            }
            search_box.Text = "";
        }

    }
}
