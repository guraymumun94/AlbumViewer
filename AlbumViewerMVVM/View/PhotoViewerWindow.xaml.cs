using AlbumViewerMVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AlbumViewerMVVM.View
{
    public partial class PhotoViewerWindow : Window
    {
        public PhotoViewerWindow()
        {
            InitializeComponent();
        }

        public bool IsClosed { get; private set; }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

        public PhotoViewerWindow(BitmapImage image)
        {
            InitializeComponent();
            PhotoViewerViewModel.image = image;
        }
    }
}
