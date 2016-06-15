//using AlbumViewerMVVM.View;
//using AlbumViewerMVVM.View;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Windows;
//using System.Windows.Forms;
//using System.Windows.Input;
//using System.Windows.Media.Imaging;

//namespace AlbumViewer.Model
//{
//    public partial class Album
//    {
//        static private List<BitmapImage> images;
//        private FolderBrowserDialog directory;
        
//        public static List<BitmapImage> Images
//        {
//            get
//            {
//                return images;
//            }

//            set
//            {
//                images = value;
//            }
//        }

//        public Album(AlbumWindow albumWindow)
//        {
//            Images = new List<BitmapImage>();
//            albumWindow.add.IsEnabled = false;
//        }

//        public void ImageMouseDoubleClick(AlbumWindow albumWindow, PhotoViewerWindow photoViewer)
//        {
//            BitmapImage selectedImage = (BitmapImage)albumWindow.listBox.SelectedItem;
//            //if (photoViewer == null || photoViewer.IsClosed)
//            //    photoViewer = new PhotoViewerWindow(selectedImage);
//            //else
//                photoViewer.image.Source = selectedImage;
//            photoViewer.Show();
//        }

//        public void OpenAlbum(AlbumWindow albumWindow)
//        {
//            if (Images.Any())
//                Images.Clear();

//            directory = new FolderBrowserDialog();
//            directory.Description = "Select an Album";
//            directory.ShowDialog();

//            if (!string.IsNullOrEmpty(directory.SelectedPath))
//            {
//                GetImages("*.jpg", albumWindow);
//                GetImages("*.png", albumWindow);

//                //albumWindow.DataContext = null;
//                //albumWindow.DataContext = Images;
//                albumWindow.listBox.ItemsSource = null;
//                albumWindow.listBox.ItemsSource = Images;

//                string albumName = directory.SelectedPath.Substring(directory.SelectedPath.LastIndexOf('\\') + 1, directory.SelectedPath.Length - directory.SelectedPath.LastIndexOf('\\') - 1);
//                albumWindow.Title = "";
//                albumWindow.Title = "Album Viewer - " + albumName;
//                albumWindow.add.IsEnabled = true;
//            }
//        }

//        private void GetImages(string extension, AlbumWindow albumWindow)
//        {
//            DirectoryInfo directoryInfo = new DirectoryInfo(directory.SelectedPath);
//            foreach (FileInfo fileName in directoryInfo.GetFiles(extension))
//            {
//                Images.Add(new BitmapImage(new Uri(fileName.FullName)));
//                //albumWindow.listBox.Items.Add(new BitmapImage(new Uri(fileName.FullName)));
//            }
//        }

//        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
//        public void AddImage(AlbumWindow albumWindow)
//        {
//            openFileDialog.Multiselect = true;
//            openFileDialog.Title = "Select images";
//            openFileDialog.Filter = "All Image Files|*.jpg;*.jpeg;*;.jpe;*.jfif;*.png|" +
//              "JPEG (*.jpg;*.jpeg;*.jpe;*.jfif;)|*.jpg;*.jpeg;.jpe;*.jfif|" +
//              "PNG (*.png)|*.png";

//            if (openFileDialog.ShowDialog() == true)
//            {
//                foreach (String fileName in openFileDialog.FileNames)
//                {
//                    Images.Add(new BitmapImage(new Uri(fileName)));
//                    //albumWindow.listBox.Items.Add(new BitmapImage(new Uri(fileName)));
//                }
//            }
//            //albumWindow.DataContext = null;
//            //albumWindow.DataContext = Images;
//            albumWindow.listBox.ItemsSource = null;
//            albumWindow.listBox.ItemsSource = Images;
//        }

//        public void NewAlbum(AlbumWindow albumWindow)
//        {
//            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to make new album?", "New Album", MessageBoxButton.YesNo, MessageBoxImage.Question);
//            if (result == MessageBoxResult.No)
//                return;

//            if (Images.Any())
//            {
//                Images.Clear();
//                //albumWindow.DataContext = null;
//                albumWindow.listBox.ItemsSource = null;
//            }
//            albumWindow.Title = "Album Viewer";
//            albumWindow.add.IsEnabled = true;
//        }

//        public void SaveAlbumAs(SaveAlbumAsWindow saveAlbum, AlbumWindow albumWindow)
//        {
//            saveAlbum = new SaveAlbumAsWindow();
//            saveAlbum.ShowDialog();

//            if (Images.Any())
//                albumWindow.add.IsEnabled = true;
//        }

//        public void DeleteButton(System.Windows.Input.KeyEventArgs e, AlbumWindow albumWindow)
//        {
//            if (e.Key == Key.Delete)
//            {
//                MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to delete selected image?", "Delete selected image", MessageBoxButton.YesNo, MessageBoxImage.Question);
//                if (result == MessageBoxResult.Yes)
//                    Images.Remove((BitmapImage)albumWindow.listBox.SelectedItem);
//            }
//            //albumWindow.DataContext = null;
//            //albumWindow.DataContext = Images;
//            albumWindow.listBox.ItemsSource = null;
//            albumWindow.listBox.ItemsSource = Images;
//        }

//        public void Exit()
//        {
//            System.Windows.Application.Current.Shutdown();
//        }
//    }
//}
