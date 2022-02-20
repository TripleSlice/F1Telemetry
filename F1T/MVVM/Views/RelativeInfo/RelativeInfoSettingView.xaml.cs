using F1T.MVVM.ViewModels;
using F1T.Settings;
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

namespace F1T.MVVM.Views.RelativeInfo
{
    /// <summary>
    /// Interaction logic for TyreSettingView.xaml
    /// </summary>
    public partial class RelativeInfoSettingView : BaseSettingView<RelativeInfoViewModel, RelativeInfoSettings>
    {
        public RelativeInfoSettingView()
        {
            InitializeComponent();
            this.DataContext = Model;
        }
        public override RelativeInfoViewModel Model { get => RelativeInfoViewModel.GetInstance(); }
    }
}
