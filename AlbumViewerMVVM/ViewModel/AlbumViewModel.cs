using AlbumViewerMVVM.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AlbumViewerMVVM.ViewModel
{
    class AlbumViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<BitmapImage> images;
        private FolderBrowserDialog directory;
        private ICommand openAlbumCommand;
        private ICommand newAlbumCommand;
        private ICommand saveAlbumAsCommand;
        private ICommand exitCommand;
        private ICommand addCommand;
        private ICommand deleteImageCommand;
        public static BitmapImage selectedImage;
        private string title;
        private bool canExecute = false;

        public AlbumViewModel()
        {
            NewAlbumCommand = new RelayCommand(NewAlbum);
            OpenAlbumCommand = new RelayCommand(OpenAlbum);
            SaveAlbumAsCommand = new RelayCommand(SaveAlbumAs);
            ExitCommand = new RelayCommand(Exit);
            AddCommand = new RelayCommand(AddImage, param => this.canExecute);
            DeleteImageCommand = new RelayCommand(DeleteButton);
            images = new ObservableCollection<BitmapImage>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public ObservableCollection<BitmapImage> Images
        {
            get
            {
                return images;
            }

            set
            {
                images = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand OpenAlbumCommand
        {
            get
            {
                return openAlbumCommand;
            }

            set
            {
                openAlbumCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand NewAlbumCommand
        {
            get
            {
                return newAlbumCommand;
            }

            set
            {
                newAlbumCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SaveAlbumAsCommand
        {
            get
            {
                return saveAlbumAsCommand;
            }

            set
            {
                saveAlbumAsCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                return exitCommand;
            }

            set
            {
                exitCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return addCommand;
            }

            set
            {
                addCommand = value;
                NotifyPropertyChanged();
            }
        }

        public BitmapImage SelectedImage
        {
            get
            {
                return selectedImage;
            }

            set
            {
                selectedImage = value;
                NotifyPropertyChanged();
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand DeleteImageCommand
        {
            get
            {
                return deleteImageCommand;
            }

            set
            {
                deleteImageCommand = value;
                NotifyPropertyChanged();
            }
        }

        public void OpenAlbum(object obj)
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

                string albumName = directory.SelectedPath.Substring(directory.SelectedPath.LastIndexOf('\\') + 1, directory.SelectedPath.Length - directory.SelectedPath.LastIndexOf('\\') - 1);
                Title = "";
                Title = "Album Viewer - " + albumName;
                canExecute = true;
            }
            NotifyPropertyChanged();
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
        public void AddImage(object obj)
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
                    //albumWindow.listBox.Items.Add(new BitmapImage(new Uri(fileName)));
                   // Images[0].UriSource
                }
            }
            NotifyPropertyChanged();
        }

        public void NewAlbum(object obj)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to make new album?", "New Album", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;

            if (Images.Any())
            {
                Images.Clear();
            }
            Title = "Album Viewer";
            canExecute = true;
            NotifyPropertyChanged();
        }

        private SaveAlbumAsWindow saveAlbum;
        public void SaveAlbumAs(object obj)
        {
            saveAlbum = new SaveAlbumAsWindow();
            saveAlbum.ShowDialog();

            if (Images.Any())
                canExecute = true;
        }

        public void DeleteButton(object obj)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Do you want to delete selected image?", "Delete selected image", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                Images.Remove(SelectedImage);
        }

        public void Exit(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }

    
}
