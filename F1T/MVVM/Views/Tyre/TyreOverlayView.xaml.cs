using F1T.MVVM.Models;
using F1T.MVVM.ViewModels;
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
    public partial class TyreOverlayView : BaseOverlayView<TyreViewModel>
    {
        public TyreOverlayView()
        {
            this.DataContext = Model;
            InitializeComponent();
            UpdateValues();

            StartTimer();
        }

        public override TyreViewModel Model { get => TyreViewModel.GetInstance(); }


        protected override void UpdateValues(object state = null)
        {
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

                        // Count how many active cars there are
                        // Result status - 0 = invalid, 1 = inactive, 2 = active
                        int carCount = 22;
                        for (int i = 0; i < AllCarLapData.Length; i++)
                        {
                            if (AllCarLapData[i].m_resultStatus != 2)
                            {
                                carCount -= 1;
                                continue;
                            }
                        }

                        LapData[] AllCarLapDataSorted = new LapData[carCount];
                        int[] index = new int[carCount];

                        // Sort the cars in the order that they are on track
                        for (int i = 0; i < AllCarLapData.Length; i++)
                        {
                            // Check if the car is still in the race
                            // Result status - 0 = invalid, 1 = inactive, 2 = active
                            if (AllCarLapData[i].m_resultStatus != 2)
                            {
                                continue;
                            }


                            try
                            {
                                var trueIndex = AllCarLapData[i].m_carPosition - 1;
                                AllCarLapDataSorted[trueIndex] = AllCarLapData[i];
                                index[trueIndex] = i;
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }

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


                        Model.TyreInfoArr.Clear();
 
                        int counter = 0;

                        for (int i = topIndex; i <= bottomIndex; i++)
                        {
                            // Wear, Age
                            int highestWear = AllCarDamageData[index[i]].m_tyresDamage.Max();
                            int tyreAge = AllCarStatusData[index[i]].m_tyresAgeLaps;
                            TyreInfo tyreInfo = new TyreInfo(highestWear, tyreAge);
                            Model.TyreInfoArr.Add(tyreInfo);

                            counter++;
                        }


                    }));
            }
        }
    }
}
