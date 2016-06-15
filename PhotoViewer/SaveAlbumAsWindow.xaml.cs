using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;

namespace AlbumViewer
{
    public partial class SaveAlbumAsWindow : Window
    {
        public SaveAlbumAsWindow()
        {
            InitializeComponent();
            canvas.Visibility = Visibility.Collapsed;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public bool IsClosed { get; private set; }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

        private FolderBrowserDialog directory;
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
        
            directory = new FolderBrowserDialog();
            directory.ShowDialog();
            if (!string.IsNullOrEmpty(directory.SelectedPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory.SelectedPath);
                txtLocation.Text = directory.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                WrongLocation();
                textBlock.Text = "Please enter a location. For example: C:\\Users\\pc\\Documents";
                return;
            }

            txtName.BorderBrush = new SolidColorBrush(Colors.Gray);
            txtLocation.BorderBrush = new SolidColorBrush(Colors.Gray);
            string folderPath = txtLocation.Text;
            folderPath = folderPath.Replace("\\", "/");

            if (txtName.Text.IndexOfAny(new char[] { ':', '/', '\\', '*', '?', '"', '<', '>', '|' }) >= 0)
            {
                WrongName();
                textBlock.Text = "A name can't contain any of the following characters: / \\ : * ? \" < > |.";
                return;
            }

            else if (Directory.Exists(folderPath + "/" + txtName.Text))
            {
                WrongName();
                textBlock.Text = "The folder " + txtName.Text + " already exists!\nPlease try another name.";
                return;
            }

            else if (string.IsNullOrEmpty(txtName.Text))
            {
                WrongName();
                textBlock.Text = "Please enter a name.";
                return;
            }

            else if (!Directory.Exists(txtLocation.Text))
            {
                WrongLocation();
                textBlock.Text = "Could not find the location: " + txtLocation.Text + ".";
                return;
            }

            else if (!Directory.Exists(folderPath + "/" + txtName.Text))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(folderPath, txtName.Text));
                txtName.BorderBrush = new SolidColorBrush(Colors.Green);
                canvas.Visibility = Visibility.Collapsed;
                Close();
            }

            foreach (var file in AlbumWindow.Images)
            {
                string temp = file.UriSource.AbsolutePath;
                int index = temp.LastIndexOf('/') + 1;
                string fileName = file.UriSource.AbsolutePath.Substring(index, temp.Length - index);
                string fullPath = file.UriSource.AbsolutePath;
                fullPath = Uri.UnescapeDataString(fullPath);
                fullPath = @System.IO.Path.GetFullPath(fullPath);
                //System.IO.File.Copy(file.UriSource.AbsolutePath.Replace("/", "\\"), @System.IO.Path.Combine(directory.SelectedPath, txtName.Text + "\\" + fileName), true);
                System.IO.File.Copy(fullPath, System.IO.Path.GetFullPath(@System.IO.Path.Combine(directory.SelectedPath, txtName.Text + "\\" + fileName)), true);
            }
        }

        private void WrongName()
        {
            txtName.BorderBrush = new SolidColorBrush(Colors.Red);
            canvas.Visibility = Visibility.Visible;
        }

        private void WrongLocation() 
        {
            txtLocation.BorderBrush = new SolidColorBrush(Colors.Red);
            canvas.Visibility = Visibility.Visible;
        }
    }
}
