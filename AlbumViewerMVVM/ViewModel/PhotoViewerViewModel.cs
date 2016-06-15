using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace AlbumViewerMVVM.ViewModel
{
    class PhotoViewerViewModel : INotifyPropertyChanged
    {
        private OpenFileDialog openFileDialog;
        public static BitmapImage image;
        private ICommand setBackgroundCommand;
        private ICommand flipCommand;
        private ICommand rotateLeftCommand;
        private ICommand rotateRightCommand;
        private ICommand nextCommand;
        private ICommand previousCommand;
        private int rotation;
        private int scaleX;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public PhotoViewerViewModel()
        {
            scaleX = 1;
            openFileDialog = new OpenFileDialog();
            SetBackgroundCommand = new RelayCommand(SetBackground);
            FlipCommand = new RelayCommand(FlipImage);
            RotateLeftCommand = new RelayCommand(RotateLeft);
            RotateRightCommand = new RelayCommand(RotateRight);
            NextCommand = new RelayCommand(NextImage);
            PreviousCommand = new RelayCommand(PreviousImage);
        }

        public BitmapImage Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
                NotifyPropertyChanged();
            }
        }

        public int Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SetBackgroundCommand
        {
            get
            {
                return setBackgroundCommand;
            }

            set
            {
                setBackgroundCommand = value;
                NotifyPropertyChanged();
            }
        }

        public int ScaleX
        {
            get
            {
                return scaleX;
            }

            set
            {
                scaleX = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand FlipCommand
        {
            get
            {
                return flipCommand;
            }

            set
            {
                flipCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand RotateLeftCommand
        {
            get
            {
                return rotateLeftCommand;
            }

            set
            {
                rotateLeftCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand RotateRightCommand
        {
            get
            {
                return rotateRightCommand;
            }

            set
            {
                rotateRightCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand NextCommand
        {
            get
            {
                return nextCommand;
            }

            set
            {
                nextCommand = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand PreviousCommand
        {
            get
            {
                return previousCommand;
            }

            set
            {
                previousCommand = value;
                NotifyPropertyChanged();
            }
        }

        public void RotateRight(object obj)
        {
            Rotation += 90;
        }

        public void NextImage(object obj)
        {
            if (ScaleX == -1)
                ScaleX = 1;

            Rotation = 0;
            BitmapImage tempImage = Image;
            int index = AlbumViewModel.images.IndexOf(tempImage);

            if (index != -1)
            {
                if (index != AlbumViewModel.images.Count - 1)
                    Image = AlbumViewModel.images[index + 1];
                else
                    Image = AlbumViewModel.images[0];
            }
        }

        public void PreviousImage(object obj)
        {
            if (ScaleX == -1)
                ScaleX = 1;

            Rotation = 0;
            BitmapImage tempImage = Image;
            int index = AlbumViewModel.images.IndexOf(tempImage);

            if (index != -1)
            {
                if (index != 0)
                    Image = AlbumViewModel.images[index - 1];
                else
                    Image = AlbumViewModel.images[AlbumViewModel.images.Count - 1];
            }
        }

        public void RotateLeft(object obj)
        {
            Rotation -= 90;
        }

        public void FlipImage(object obj)
        {
            if (ScaleX == 1)
                ScaleX = -1;
            else
                ScaleX = 1;
        }

        //Set as desktop background
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);
        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

        public void SetBackground(object obj)
        {
            BitmapImage background = Image;
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, Uri.UnescapeDataString(background.UriSource.AbsolutePath), SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}
