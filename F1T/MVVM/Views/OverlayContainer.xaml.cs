using F1T.Core;
using F1T.MVVM.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace F1T.MVVM.Views
{
    /// <summary>
    /// Interaction logic for OverlayContainer.xaml
    /// </summary>
    public partial class OverlayContainer : Window
    {


        public OverlayContainerViewModel Model = new OverlayContainerViewModel();

        public OverlayContainer(UserControl overlayView, BaseModuleViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
            ContentHolder.DataContext = Model;
            Model.CurrentOverlay = overlayView;
        }

        public void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Get window associated with object being clicked
                DependencyObject element = (DependencyObject)sender;
                Window window = Window.GetWindow(element);

                if (e.ChangedButton == MouseButton.Left)
                    window.DragMove();
            }
            catch (Exception) { }
        }
    }
}
