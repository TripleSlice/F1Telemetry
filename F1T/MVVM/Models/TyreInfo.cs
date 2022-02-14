using F1T.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.MVVM.Models
{
    /// <summary>
    /// Model for info regarding Tyres
    /// </summary>
    public class TyreInfo : ObservableObject
    {
        /// <summary>
        /// Constructs a <see cref="TyreInfo"/> object
        /// </summary>
        /// <param name="wear"></param>
        /// <param name="age"></param>
        public TyreInfo(int wear, int age)
        {
            TyreWear = wear;
            TyreAge = age;
        }

        private int _tyreAge;
        /// <summary>
        /// The age of the tyres in laps
        /// <para>A new start starts at 0</para>
        /// </summary>
        public int TyreAge
        {
            get { return _tyreAge; }
            set { SetField(ref _tyreAge, value, "TyreAge"); }
        }

        private int _tyreWear;
        /// <summary>
        /// The wear of the tyre as a percentage (0-100)
        /// </summary>
        public int TyreWear
        {
            get { return _tyreWear; }
            set { SetField(ref _tyreWear, value, "TyreWear"); }
        }

    }
}
