using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hw_16_09_23
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int _index;
        string[] _path;
        public MainWindow()
        {
            InitializeComponent();

            _index = 0;
            _path = Directory.GetFiles(@"G:\ШАГ\WPF\_hw\hw_16_09_23\hw_16_09_23\img");

            FullImage.Source = GetPath();
            CreatePreviewImage();
        }

        private void GridLeft_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_index - 1 >= 0)
            {
                GridLeft.Opacity = 1;
            }
        }
        private void GridLeft_MouseLeave(object sender, MouseEventArgs e)
        {
            GridLeft.Opacity = 0;
        }
        private void GridRight_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_index + 1 != _path.Length)
            {
                GridRight.Opacity = 1;
            }
        }
        private void GridRight_MouseLeave(object sender, MouseEventArgs e)
        {
            GridRight.Opacity = 0;
        }
        BitmapImage GetPath()
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(_path[_index]);
            bitmapImage.EndInit();
            return bitmapImage;
        }
        private void DirectionForward()
        {
            if (_index + 1 <= _path.Length - 1)
            {
                _index++;
                FullImage.Source = GetPath();
            }
        }
        private void DirectionBack()
        {
            if (_index - 1 >= 0)
            {
                _index--;
                FullImage.Source = GetPath();
            }
        }
        private void HideArrows()
        {
            if (_index - 1 < 0)
            {
                GridLeft.Opacity = 0;
            }
            else if (_index + 1 == _path.Length)
            {
                GridRight.Opacity = 0;
            }
        }
        private void GridLeft_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DirectionBack();
        }
        private void GridRight_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DirectionForward();
        }
        private void GridLeft_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HideArrows();
        }
        private void GridRight_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            HideArrows();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                DirectionBack();
            }
            else if (e.Key == Key.Right)
            {
                DirectionForward();
            }

            HideArrows();
        }
        private void CreatePreviewImage()
        {
            foreach (var path in _path)
            {
                Image image = new Image
                {
                    Source = new BitmapImage(new Uri(path)),
                    Margin = new Thickness(5),
                    Height = 100
                };

                image.MouseLeftButtonDown += Image_MouseLeftButtonDown;
                ArrayImg.Children.Add(image);
            }
        }
        private void SPPopup_MouseEnter(object sender, MouseEventArgs e)
        {
            SPPopup.Opacity = 1;
        }
        private void SPPopup_MouseLeave(object sender, MouseEventArgs e)
        {
            SPPopup.Opacity = 0;
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = (Image)sender;
            FullImage.Source = img.Source;

            string str = img.Source.ToString();
            str = str.Substring(str.LastIndexOf('/') + 1);

            for (int i = 0; i < _path.Length; i++)
            {
                if (string.Compare(_path[i].Substring(_path[i].LastIndexOf('\\') + 1), str) == 0)
                {
                    _index = i;
                }
            }
        }
    }
}
