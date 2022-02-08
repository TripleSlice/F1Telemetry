using F1T.MVVM.Views.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.MVVM.ViewModels
{
    public class HomeViewModel
    {
        // === BEGINING OF MODULE SETUP
        // === Singleton Instance with Thread Saftey ===
        private static HomeViewModel _instance = null;
        private static object _singletonLock = new object();
        public static HomeViewModel GetInstance()
        {
            lock (_singletonLock)
            {
                if (_instance == null) { _instance = new HomeViewModel(); }
                return _instance;
            }
        }
        // === END OF MODULE SETUP ===
    }
}
