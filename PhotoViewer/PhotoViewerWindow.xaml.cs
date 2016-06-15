using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AlbumViewer
{
    public partial class PhotoViewerWindow : Window
    {
        private OpenFileDialog openFileDialog;

        private int rotation;
        public int Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
            }
        }
        
        public PhotoViewerWindow()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
        }

        public PhotoViewerWindow(BitmapImage image)
        {
            InitializeComponent();
            this.image.Source = image;
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            rotateTransform.Angle = (Rotation += 90);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (scaleTransform.ScaleX == -1)
                scaleTransform.ScaleX = 1;

            rotateTransform.Angle = (Rotation = 0);
            BitmapImage tempImage = (BitmapImage)image.Source;
            int index = AlbumWindow.Images.IndexOf(tempImage);

            if (index != -1)
            {
                if (index != AlbumWindow.Images.Count - 1)
                    image.Source = AlbumWindow.Images[index + 1];
                else
                    image.Source = AlbumWindow.Images[0];
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (scaleTransform.ScaleX == -1)
                scaleTransform.ScaleX = 1;

            rotateTransform.Angle = (Rotation = 0);
            BitmapImage tempImage = (BitmapImage)image.Source;
            int index = AlbumWindow.Images.IndexOf(tempImage);

            if (index != -1)
            {
                if (index != 0)
                    image.Source = AlbumWindow.Images[index - 1];
                else
                    image.Source = AlbumWindow.Images[AlbumWindow.Images.Count - 1];
            }
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            rotateTransform.Angle = (Rotation -= 90);
        }

        public bool IsClosed { get; private set; }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

        private void btnFlip_Click(object sender, RoutedEventArgs e)
        {
            if (scaleTransform.ScaleX == 1)
                scaleTransform.ScaleX = -1;
            else
                scaleTransform.ScaleX = 1;
        }

        //Set as desktop background
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);
        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;
        private void btnBackground_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage background = (BitmapImage)image.Source;
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, Uri.UnescapeDataString(background.UriSource.AbsolutePath), SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}
