using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace F1T.MVVM.Views.Setup
{
    /// <summary>
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window, INotifyPropertyChanged
    {

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);



        readonly Rectangle screenRectangle;

        private string _imageSource;
        public string ImageSource
        {
            get { return _imageSource; }
            set { SetField(ref _imageSource, value, "ImageSource"); }
        }

        public ImageWindow(String source, Screen screen)
        {
            ImageSource = source;
            screenRectangle = screen.WorkingArea;

            InitializeComponent();
            this.DataContext = this;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        // boiler-plate
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var wih = new WindowInteropHelper(this);
            IntPtr hWnd = wih.Handle;
            MoveWindow(hWnd, screenRectangle.Left, screenRectangle.Top, screenRectangle.Width, screenRectangle.Height, false);
        }
    }
}
