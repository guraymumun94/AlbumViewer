using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace AlbumViewerMVVM.ViewModel
{
    class SaveAlbumAsViewModel : INotifyPropertyChanged
    {
        private FolderBrowserDialog directory;
        private string location;
        private string name;
        private ICommand okCommand;
        private ICommand browseCommand;
        private ICommand cancelCommand;
        private SolidColorBrush nameColor;
        private SolidColorBrush locationColor;
        private string errorText;
        private Visibility vis;

        public SaveAlbumAsViewModel()
        {
            errorText = "Please enter a location. For example: C:\\Users\\pc\\Documents";
            name = "";
            location = "";
            nameColor = new SolidColorBrush(Colors.Gray);
            locationColor = new SolidColorBrush(Colors.Gray);
            vis = Visibility.Hidden;
            OkCommand = new RelayCommand(OK);
            BrowseCommand = new RelayCommand(Browse);
            CancelCommand = new RelayCommand(Cancel);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                NotifyPropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand OkCommand
        {
            get
            {
                return okCommand;
            }

            set
            {
                okCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand BrowseCommand
        {
            get
            {
                return browseCommand;
            }

            set
            {
                browseCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand;
            }

            set
            {
                cancelCommand = value;
                NotifyPropertyChanged();
            }
        }        

        public SolidColorBrush NameColor
        {
            get
            {
                return nameColor;
            }

            set
            {
                nameColor = value;
                NotifyPropertyChanged();
            }
        }

        public SolidColorBrush LocationColor
        {
            get
            {
                return locationColor;
            }

            set
            {
                locationColor = value;
                NotifyPropertyChanged();
            }
        }

        public string ErrorText
        {
            get
            {
                return errorText;
            }

            set
            {
                errorText = value;
                NotifyPropertyChanged();
            }
        }

        public Visibility Vis
        {
            get
            {
                return vis;
            }

            set
            {
                vis = value;
                NotifyPropertyChanged();
            }
        }

        public void Cancel(object obj)
        {

        }

        public void Browse(object obj)
        {
            directory = new FolderBrowserDialog();
            directory.ShowDialog();
            if (!string.IsNullOrEmpty(directory.SelectedPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory.SelectedPath);
                Location = directory.SelectedPath;
            }
        }
        private void WrongName()
        {
            nameColor = new SolidColorBrush(Colors.Red);
            vis = Visibility.Visible;
        }

        private void WrongLocation()
        {
            locationColor = new SolidColorBrush(Colors.Red);
            vis = Visibility.Visible;
        }

        public void OK(object obj)
        {
            if (string.IsNullOrEmpty(location))
            {
                errorText = "Please enter a location. For example: C:\\Users\\pc\\Documents";
                WrongName();
                NotifyPropertyChanged();
                return;
            }

            nameColor = new SolidColorBrush(Colors.Gray);
            locationColor = new SolidColorBrush(Colors.Gray);
            string folderPath = location;
            folderPath = folderPath.Replace("\\", "/");

            if (name.IndexOfAny(new char[] { ':', '/', '\\', '*', '?', '"', '<', '>', '|' }) >= 0)
            {
                errorText = "A name can't contain any of the following characters: / \\ : * ? \" < > |.";
                WrongName();
                NotifyPropertyChanged();
                return;
            }

            else if (Directory.Exists(folderPath + "/" + name))
            {
                WrongName();
                errorText = "The folder " + name + " already exists!\nPlease try another name.";
                NotifyPropertyChanged();
                return;
            }

            else if (string.IsNullOrEmpty(name))
            {
                WrongName();
                errorText = "Please enter a name.";
                NotifyPropertyChanged();
                return;
            }

            else if (!Directory.Exists(location))
            {
                WrongName();
                errorText = "Could not find the location: " + location + ".";
                NotifyPropertyChanged();
                return;
            }

            else if (!Directory.Exists(folderPath + "/" + name))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(folderPath, name));
                nameColor = new SolidColorBrush(Colors.Green);
                vis = Visibility.Hidden;
                System.Windows.Application.Current.Shutdown();
            }

            foreach (var file in AlbumViewModel.images)
            {
                string temp = file.UriSource.AbsolutePath;
                int index = temp.LastIndexOf('/') + 1;
                string fileName = file.UriSource.AbsolutePath.Substring(index, temp.Length - index);
                string fullPath = file.UriSource.AbsolutePath;
                fullPath = Uri.UnescapeDataString(fullPath);
                fullPath = @System.IO.Path.GetFullPath(fullPath);
                //System.IO.File.Copy(file.UriSource.AbsolutePath.Replace("/", "\\"), @System.IO.Path.Combine(directory.SelectedPath, txtName.Text + "\\" + fileName), true);
                System.IO.File.Copy(fullPath, System.IO.Path.GetFullPath(@System.IO.Path.Combine(directory.SelectedPath, name + "\\" + fileName)), true);
            }
            NotifyPropertyChanged();
        }
    }
}
