using AlbumViewerMVVM.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AlbumViewerMVVM.View
{
    public partial class AlbumWindow : Window
    {
        PhotoViewerWindow photoViewer;
        public AlbumWindow()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!AlbumViewModel.images.Any())
                return;

            if (photoViewer == null || photoViewer.IsClosed)
                photoViewer = new PhotoViewerWindow(AlbumViewModel.selectedImage);
            else
                PhotoViewerViewModel.image = AlbumViewModel.selectedImage;

            if (AlbumViewModel.selectedImage == null)
                return;

            photoViewer = new PhotoViewerWindow();
            photoViewer.Show();
        }
    }
}
