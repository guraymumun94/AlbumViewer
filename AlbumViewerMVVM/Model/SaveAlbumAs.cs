//using System;
//using System.IO;
//using System.Windows;
//using System.Windows.Forms;
//using System.Windows.Media;
//using AlbumViewerMVVM;
//using AlbumViewer.View;

//namespace AlbumViewer.Model
//{
//    public partial class SaveAlbumAs
//    {
//        public SaveAlbumAs(SaveAlbumAsWindow saveAlbumAs)
//        {
//            saveAlbumAs.canvas.Visibility = Visibility.Collapsed;
//        }

//        public void Cancel(SaveAlbumAsWindow saveAlbumAs)
//        {
//            saveAlbumAs.Close();
//        }

//        private FolderBrowserDialog directory;
//        public void Browse(SaveAlbumAsWindow saveAlbumAs)
//        {
//            directory = new FolderBrowserDialog();
//            directory.ShowDialog();
//            if (!string.IsNullOrEmpty(directory.SelectedPath))
//            {
//                DirectoryInfo directoryInfo = new DirectoryInfo(directory.SelectedPath);
//                saveAlbumAs.txtLocation.Text = directory.SelectedPath;
//            }
//        }
//        private void WrongName(SaveAlbumAsWindow saveAlbumAs)
//        {
//            saveAlbumAs.txtName.BorderBrush = new SolidColorBrush(Colors.Red);
//            saveAlbumAs.canvas.Visibility = Visibility.Visible;
//        }

//        private void WrongLocation(SaveAlbumAsWindow saveAlbumAs)
//        {
//            saveAlbumAs.txtLocation.BorderBrush = new SolidColorBrush(Colors.Red);
//            saveAlbumAs.canvas.Visibility = Visibility.Visible;
//        }

//        public void OK(SaveAlbumAsWindow saveAlbumAs)
//        {
//            if (string.IsNullOrEmpty(saveAlbumAs.txtLocation.Text))
//            {
//                WrongName(saveAlbumAs);
//                saveAlbumAs.textBlock.Text = "Please enter a location. For example: C:\\Users\\pc\\Documents";
//                return;
//            }

//            saveAlbumAs.txtName.BorderBrush = new SolidColorBrush(Colors.Gray);
//            saveAlbumAs.txtLocation.BorderBrush = new SolidColorBrush(Colors.Gray);
//            string folderPath = saveAlbumAs.txtLocation.Text;
//            folderPath = folderPath.Replace("\\", "/");

//            if (saveAlbumAs.txtName.Text.IndexOfAny(new char[] { ':', '/', '\\', '*', '?', '"', '<', '>', '|' }) >= 0)
//            {
//                WrongName(saveAlbumAs);
//                saveAlbumAs.textBlock.Text = "A name can't contain any of the following characters: / \\ : * ? \" < > |.";
//                return;
//            }

//            else if (Directory.Exists(folderPath + "/" + saveAlbumAs.txtName.Text))
//            {
//                WrongName(saveAlbumAs);
//                saveAlbumAs.textBlock.Text = "The folder " + saveAlbumAs.txtName.Text + " already exists!\nPlease try another name.";
//                return;
//            }

//            else if (string.IsNullOrEmpty(saveAlbumAs.txtName.Text))
//            {
//                WrongName(saveAlbumAs);
//                saveAlbumAs.textBlock.Text = "Please enter a name.";
//                return;
//            }

//            else if (!Directory.Exists(saveAlbumAs.txtLocation.Text))
//            {
//                WrongName(saveAlbumAs);
//                saveAlbumAs.textBlock.Text = "Could not find the location: " + saveAlbumAs.txtLocation.Text + ".";
//                return;
//            }

//            else if (!Directory.Exists(folderPath + "/" + saveAlbumAs.txtName.Text))
//            {
//                Directory.CreateDirectory(System.IO.Path.Combine(folderPath, saveAlbumAs.txtName.Text));
//                saveAlbumAs.txtName.BorderBrush = new SolidColorBrush(Colors.Green);
//                saveAlbumAs.canvas.Visibility = Visibility.Collapsed;
//                saveAlbumAs.Close();
//            }

//            foreach (var file in Album.Images)
//            {
//                string temp = file.UriSource.AbsolutePath;
//                int index = temp.LastIndexOf('/') + 1;
//                string fileName = file.UriSource.AbsolutePath.Substring(index, temp.Length - index);
//                string fullPath = file.UriSource.AbsolutePath;
//                fullPath = Uri.UnescapeDataString(fullPath);
//                fullPath = @System.IO.Path.GetFullPath(fullPath);
//                //System.IO.File.Copy(file.UriSource.AbsolutePath.Replace("/", "\\"), @System.IO.Path.Combine(directory.SelectedPath, txtName.Text + "\\" + fileName), true);
//                System.IO.File.Copy(fullPath, System.IO.Path.GetFullPath(@System.IO.Path.Combine(directory.SelectedPath, saveAlbumAs.txtName.Text + "\\" + fileName)), true);
//            }
//        }
//    }
//}
