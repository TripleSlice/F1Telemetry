using F1T.MVVM.ViewModels;
using F1T.Themes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace F1T.MVVM.Views.Tyre
{
    /// <summary>
    /// Interaction logic for TyreSettingView.xaml
    /// </summary>
    public partial class TyreSettingView : BaseSettingView<TyreViewModel>
    {
        public TyreSettingView()
        {
            InitializeComponent();
            this.DataContext = Model;
        }
        public override TyreViewModel Model { get => TyreViewModel.GetInstance(); }
    }
}
