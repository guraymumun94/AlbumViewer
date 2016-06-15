using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace AlbumViewer
{
    public partial class AlbumWindow : Window
    {
        static private List<BitmapImage> images;
        private FolderBrowserDialog directory;

        public static List<BitmapImage> Images
        {
            get
            {
                return images;
            }

            set
            {
                images = value;
            }
        }


        private PhotoViewerWindow photoViewer;
        private SaveAlbumAsWindow saveAlbum;

        public AlbumWindow()
        {
            InitializeComponent();
            photoViewer = new PhotoViewerWindow();
            saveAlbum = new SaveAlbumAsWindow();
            Images = new List<BitmapImage>();
            add.IsEnabled = false;
            loading.Visibility = Visibility.Hidden;
            //bar.Visibility = Visibility.Hidden;
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!Images.Any())
                return;
            BitmapImage selectedImage = (BitmapImage)listBox.SelectedItem;
            if (photoViewer == null || photoViewer.IsClosed)
                photoViewer = new PhotoViewerWindow(selectedImage);
            else
                photoViewer.image.Source = selectedImage;

            if (listBox.SelectedItem == null)
                return;
            photoViewer.Show();
        }

        BackgroundWorker worker = new BackgroundWorker();
        private async void open_Click(object sender, RoutedEventArgs e)
        {
            //worker.DoWork += DoIndependentWork;
            //worker.RunWorkerCompleted += OpenAlbum;
            //worker.RunWorkerAsync();
            //DoIndependentWork();   
            await OpenAlbum();
            //DoIndependentWork();

            //bar.Visibility = Visibility.Hidden;
            //loading.Visibility = Visibility.Hidden;
        }

        async Task OpenAlbum()
        {

            Dispatcher.Invoke((Action)( () =>
            {
                if (Images.Any())
                    Images.Clear();
                
                directory = new FolderBrowserDialog();
                directory.Description = "Select an Album";
                directory.ShowDialog();


                if (!string.IsNullOrEmpty(directory.SelectedPath))
                {
                    GetImages("*.jpg");
                    GetImages("*.png");

                    //DataContext = null;
                    //DataContext = Images;
                    //await Task.Run(() =>
                    //{

                        string albumName = directory.SelectedPath.Substring(directory.SelectedPath.LastIndexOf('\\') + 1, directory.SelectedPath.Length - directory.SelectedPath.LastIndexOf('\\') - 1);
                        Title = "";
                        Title = "Album Viewer - " + albumName;
                        add.IsEnabled = true;
                        //loading.Visibility = Visibility.Hidden;
                        listBox.ItemsSource = null;
                        listBox.ItemsSource = Images;

                    //});
                }
            }));
            //return Images;



        }

        private void DoIndependentWork()
        {

            Dispatcher.Invoke((Action)(() =>
            {
                
                //bar.Visibility = Visibility.Visible;
                loading.Visibility = Visibility.Visible;
            }));

        }

        private void GetImages(string extension)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory.SelectedPath);
            foreach (FileInfo fileName in directoryInfo.GetFiles(extension))
            {
                Images.Add(new BitmapImage(new Uri(fileName.FullName)));
                //albumWindow.listBox.Items.Add(new BitmapImage(new Uri(fileName.FullName)));
            }
        }

        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
        private void add_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "Select images";
            openFileDialog.Filter = "All Image Files|*.jpg;*.jpeg;*;.jpe;*.jfif;*.png|" +
              "JPEG (*.jpg;*.jpeg;*.jpe;*.jfif;)|*.jpg;*.jpeg;.jpe;*.jfif|" +
              "PNG (*.png)|*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (String fileName in openFileDialog.FileNames)
                {
                    Images.Add(new BitmapImage(new Uri(fileName)));
                    //listBox.Items.Add(new BitmapImage(new Uri(fileName)));
                }
            }
            //DataContext = null;
            //DataContext = Images;
            listBox.ItemsSource = null;
            listBox.ItemsSource = Images;
        }

        private void new_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to make new album?", "New Album", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;

            if (Images.Any())
            {
                Images.Clear();
                //DataContext = null;
                listBox.ItemsSource = null;
            }
            Title = "Album Viewer";
            add.IsEnabled = true;
        }

        private void saveAs_Click(object sender, RoutedEventArgs e)
        {
            saveAlbum = new SaveAlbumAsWindow();
            saveAlbum.ShowDialog();

            if (Images.Any())
                add.IsEnabled = true;
        }

        private void listBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to delete selected image?", "Delete selected image", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    Images.Remove((BitmapImage)listBox.SelectedItem);
            }
            //DataContext = null;
            //DataContext = Images;
            listBox.ItemsSource = null;
            listBox.ItemsSource = Images;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        protected override void OnClosed(EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
