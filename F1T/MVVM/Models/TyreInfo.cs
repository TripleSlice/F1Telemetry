using F1T.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.MVVM.Models
{
    public class TyreInfo : ObservableObject
    {
        public TyreInfo(int wear, int age)
        {
            TyreWear = wear;
            TyreAge = age;
        }

        private int _tyreAge;
        public int TyreAge
        {
            get { return _tyreAge; }
            set { SetField(ref _tyreAge, value, "TyreAge"); }
        }

        private int _tyreWear;
        public int TyreWear
        {
            get { return _tyreWear; }
            set { SetField(ref _tyreWear, value, "TyreWear"); }
        }

    }
}
