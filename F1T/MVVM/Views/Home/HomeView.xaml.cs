using F1T.MVVM.ViewModels;
using System.Windows.Controls;

namespace F1T.MVVM.Views.Home
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        HomeViewModel Model = new HomeViewModel();
        public HomeView()
        {
            InitializeComponent();
            this.DataContext = Model;
        }
    }
}
