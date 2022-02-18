using F1T.MVVM.Models;
using F1T.MVVM.ViewModels;
using F1T.Settings;
using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace F1T.MVVM.Views.Tyre
{
    /// <summary>
    /// Interaction logic for TyreOverlayView.xaml
    /// </summary>
    public partial class TyreOverlayView : BaseOverlayView<TyreViewModel, TyreSettings>
    {

        private int[] IndexToPositionArr = new int[22];

        public TyreOverlayView()
        {
            this.DataContext = Model;
            InitializeComponent();
            UpdateValues();

            StartTimer();
        }


        public override TyreViewModel Model { get => TyreViewModel.GetInstance(); }

        /// <summary>
        /// Get how many cars are left in the race
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private int GetActiveCarCount(LapData[] data)
        {
            // Count how many active cars there are
            int carCount = 22;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].m_resultStatus == ResultStatus.Invalid || data[i].m_resultStatus == ResultStatus.Inactive)
                {
                    carCount -= 1;
                    continue;
                }
            }
            return carCount;
        }

        /// <summary>
        /// Translates the <see cref="LapData"/> array into a int[] with indexs which point to the position in the UDP arrays
        /// <para> Example [2,4,3,0,1] means LapData[2] is in first, Lapdata[1] is in last... etc</para>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="carCount"></param>
        /// <returns></returns>
        private void UpdatePositionArray(LapData[] data)
        {
            // Sort the cars in the order that they are on track
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].m_resultStatus == ResultStatus.Invalid || data[i].m_resultStatus == ResultStatus.Inactive)
                {
                    continue;
                }

                var trueIndex = data[i].m_carPosition - 1;

                // For some reason a DNF'ed car is still considered active...
                try
                {
                    IndexToPositionArr[trueIndex] = i;
                }catch (Exception) { }
            }
        }

        private T GetByRealPosition<T>(T[] data, int[] indexArr, int position)
        {
            return data[indexArr[position - 1]];
        }

        protected override void UpdateValues(object state = null)
        {

            if (Model.Settings.Frequency != currentFrequency)
            {
                StopTimer();
                StartTimer();
            }

            if (Model.OverlayVisible && Model.PlayerIndexCarStatus != -1 && Model.PlayerIndexCarDamage != -1 && Model.PlayerIndexLapData != -1)
            {
                // Must start this here, or else we could be dealing with de-sync between values of Model and this code...
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        LapData PlayerCar = Model.PlayerCarLapData;
                        LapData[] AllCarLapData = Model.LapData.m_lapData;
                        CarDamageData[] AllCarDamageData = Model.CarDamageData.m_carDamageData;
                        CarStatusData[] AllCarStatusData = Model.CarStatusData.m_carStatusData;

                        UpdatePositionArray(AllCarLapData);

                        var carCount = GetActiveCarCount(AllCarLapData);
                        int carIndex = PlayerCar.m_carPosition - 1;
                        int topIndex = carIndex - 2;
                        int bottomIndex = carIndex + 2;

                        if (topIndex < 0)
                        {
                            topIndex -= topIndex;
                            bottomIndex = topIndex + 4;
                        }else if(bottomIndex > carCount - 1)
                        {
                            topIndex = carCount - 5;
                            bottomIndex = topIndex + 4;
                        }

                        if (carCount < bottomIndex) bottomIndex = carCount - 1;

                        // Causing memory leaks
                        //Model.TyreInfoArr.Clear();

                        var count = 0;

                        for (int i = topIndex; i <= bottomIndex; i++)
                        {
                            // Wear, Age
                            int highestWear = GetByRealPosition(AllCarDamageData, IndexToPositionArr, i + 1).m_tyresDamage.Max();
                            int tyreAge = GetByRealPosition(AllCarStatusData, IndexToPositionArr, i + 1).m_tyresAgeLaps;
                            TyreInfo tyreInfo = new TyreInfo(highestWear, tyreAge);
                            Model.TyreInfoArr[count] = tyreInfo;
                            // This was causing memory leaks
                            //Model.TyreInfoArr.Add(tyreInfo);
                            count++;
                        }

                        for(int i = count; i< 5; i++)
                        {
                            TyreInfo tyreInfo = new TyreInfo(0, 0);
                            Model.TyreInfoArr[count] = tyreInfo;
                            count++;
                        }

                    }));

            }
        }
    }
}
