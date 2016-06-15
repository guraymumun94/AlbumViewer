//using Microsoft.Win32;
//using System;
//using System.Windows.Media.Imaging;
//using System.Runtime.InteropServices;
//using AlbumViewer.View;

//namespace AlbumViewerMVVM.Model
//{
//    public partial class PhotoViewer
//    {
//        private OpenFileDialog openFileDialog;

//        public PhotoViewer()
//        {
//            openFileDialog = new OpenFileDialog();
//        }

//        private int rotation;
//        public int Rotation
//        {
//            get
//            {
//                return rotation;
//            }

//            set
//            {
//                rotation = value;
//            }
//        }

//        public void RotateRight(PhotoViewerWindow photoViewer)
//        {
//            photoViewer.rotateTransform.Angle = (Rotation += 90);
//        }

//        public void NextImage(PhotoViewerWindow photoViewer)
//        {
//            if (photoViewer.scaleTransform.ScaleX == -1)
//                photoViewer.scaleTransform.ScaleX = 1;

//            photoViewer.rotateTransform.Angle = (Rotation = 0);
//            BitmapImage tempImage = (BitmapImage)photoViewer.image.Source;
//            int index = Album.Images.IndexOf(tempImage);

//            if (index != -1)
//            {
//                if (index != Album.Images.Count - 1)
//                    photoViewer.image.Source = Album.Images[index + 1];
//                else
//                    photoViewer.image.Source = Album.Images[0];
//            }
//        }

//        public void PreviousImage(PhotoViewerWindow photoViewer)
//        {
//            if (photoViewer.scaleTransform.ScaleX == -1)
//                photoViewer.scaleTransform.ScaleX = 1;

//            photoViewer.rotateTransform.Angle = (Rotation = 0);
//            BitmapImage tempImage = (BitmapImage)photoViewer.image.Source;
//            int index = Album.Images.IndexOf(tempImage);

//            if (index != -1)
//            {
//                if (index != 0)
//                    photoViewer.image.Source = Album.Images[index - 1];
//                else
//                    photoViewer.image.Source = Album.Images[Album.Images.Count - 1];
//            }
//        }

//        public void RotateLeft(PhotoViewerWindow photoViewer)
//        {
//            photoViewer.rotateTransform.Angle = (Rotation -= 90);
//        }

//        public void FlipImage(PhotoViewerWindow photoViewer)
//        {
//            if (photoViewer.scaleTransform.ScaleX == 1)
//                photoViewer.scaleTransform.ScaleX = -1;
//            else
//                photoViewer.scaleTransform.ScaleX = 1;
//        }

//        //Set as desktop background
//        [DllImport("user32.dll", CharSet = CharSet.Auto)]
//        private static extern Int32 SystemParametersInfo(UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);
//        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
//        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
//        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

//        public void SetBackground(PhotoViewerWindow photoViewer)
//        {
//            BitmapImage background = (BitmapImage)photoViewer.image.Source;
//            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, Uri.UnescapeDataString(background.UriSource.AbsolutePath), SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
//        }
//    }
//}
