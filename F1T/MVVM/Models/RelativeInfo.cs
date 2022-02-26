using F1T.Core;
using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.MVVM.Models
{
    /// <summary>
    /// Model for info regarding Tyres
    /// </summary>
    public class RelativeInfo : ObservableObject
    {
        /// <summary>
        /// Constructs a <see cref="RelativeInfo"/> object
        /// </summary>
        /// <param name="wear"></param>
        /// <param name="age"></param>
        public RelativeInfo()
        {
            TyreWear = 0;
            TyreAge = 0;
            LastLapTime = 0;
            CurrentLapTime = 0;
            FastestLapTime = 0;
            S1Time = 0;
            S2Time = 0;
            S3Time = 0;

            TyreTypes = new ObservableCollection<VisualTyreCompound>();
            // Game gives data for last 8 tyre changes
            TyreTypes.Add(VisualTyreCompound.Unknown);
            TyreTypes.Add(VisualTyreCompound.Unknown);
            TyreTypes.Add(VisualTyreCompound.Unknown);
            TyreTypes.Add(VisualTyreCompound.Unknown);
            TyreTypes.Add(VisualTyreCompound.Unknown);
            TyreTypes.Add(VisualTyreCompound.Unknown);
            TyreTypes.Add(VisualTyreCompound.Unknown);
            TyreTypes.Add(VisualTyreCompound.Unknown);

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

        private uint _lastLapTime;
        public uint LastLapTime
        {
            get { return _lastLapTime; }
            set { SetField(ref _lastLapTime, value, "LastLapTime"); }
        }

        private uint _currentLapTime;
        public uint CurrentLapTime
        {
            get { return _currentLapTime; }
            set { SetField(ref _currentLapTime, value, "CurrentLapTime"); }
        }

        private uint _fastestLapTime;
        public uint FastestLapTime
        {
            get { return _fastestLapTime; }
            set { SetField(ref _fastestLapTime, value, "FastestLapTime"); }
        }

        private ushort _s1Time;
        public ushort S1Time
        {
            get { return _s1Time; }
            set { SetField(ref _s1Time, value, "S1Time"); }
        }

        private ushort _s2Time;
        public ushort S2Time
        {
            get { return _s2Time; }
            set { SetField(ref _s2Time, value, "S2Time"); }
        }

        private ushort _s3Time;
        public ushort S3Time
        {
            get { return _s3Time; }
            set { SetField(ref _s3Time, value, "S3Time"); }
        }

        private ObservableCollection<VisualTyreCompound> _tyreTypes;
        public ObservableCollection<VisualTyreCompound> TyreTypes
        {
            get { return _tyreTypes; }
            set { SetField(ref _tyreTypes, value, "TyreTypes"); }
        }
    }
}
