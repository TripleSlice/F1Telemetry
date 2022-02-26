using F1T.MVVM.Models;
using F1T.MVVM.ViewModels;
using F1T.Settings;
using F1T.Structs;
using F1T.Utils;
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

namespace F1T.MVVM.Views.RelativeInfo
{
    /// <summary>
    /// Interaction logic for TyreOverlayView.xaml
    /// </summary>
    public partial class RelativeInfoOverlayView : BaseOverlayView<RelativeInfoViewModel, RelativeInfoSettings>
    {

        private int[] IndexToPositionArr = new int[22];

        public RelativeInfoOverlayView()
        {
            this.DataContext = Model;
            InitializeComponent();
            UpdateValues();

            StartTimer();
        }

        public override RelativeInfoViewModel Model { get => RelativeInfoViewModel.GetInstance(); }

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

                        LapDataUtils.UpdatePositionArray(AllCarLapData, ref IndexToPositionArr);


                        // TODO this can be abstracted into IndexingUtils
                        var carCount = LapDataUtils.GetActiveCarCount(AllCarLapData);
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

                        var count = 0;

                        for (int i = topIndex; i <= bottomIndex; i++)
                        {
                            // Wear, Age, Laptime, Sector times
                            int highestWear = IndexingUtils.GetByRealPosition(AllCarDamageData, IndexToPositionArr, i + 1).m_tyresDamage.Max();
                            int tyreAge = IndexingUtils.GetByRealPosition(AllCarStatusData, IndexToPositionArr, i + 1).m_tyresAgeLaps;
                            uint currentLapTime = IndexingUtils.GetByRealPosition(AllCarLapData, IndexToPositionArr, i + 1).m_currentLapTimeInMS;
                            uint lastLapTime = IndexingUtils.GetByRealPosition(AllCarLapData, IndexToPositionArr, i + 1).m_lastLapTimeInMS;
                            ushort s1Time = IndexingUtils.GetByRealPosition(AllCarLapData, IndexToPositionArr, i + 1).m_sector1TimeInMS;
                            ushort s2Time = IndexingUtils.GetByRealPosition(AllCarLapData, IndexToPositionArr, i + 1).m_sector2TimeInMS;
                            ushort s3Time = (ushort)(currentLapTime - (s1Time + s2Time));
 
                            uint fastestLapTime = 0;
                            PacketSessionHistoryData packet;
                            var index = IndexingUtils.GetRealIndex(IndexToPositionArr, i + 1);

                            if (Model.SessionHistoryData.TryGetValue(index, out packet))
                            {
                                // prevent index out of bound
                                if (packet.m_lapHistoryData.Count() <= packet.m_bestLapTimeLapNum - 1) continue;

                                if (packet.m_bestLapTimeLapNum >= 1) fastestLapTime = packet.m_lapHistoryData[packet.m_bestLapTimeLapNum - 1].m_lapTimeInMS;

                                // TODO make the 3 configurable to show how many tyres 
                                // 0 is the oldest tyre
                                var newestTyreIndex = -1;
                                for (int j = packet.m_tyreStintsHistoryData.Count() - 1; j >= 0; j--)
                                {
                                    if(packet.m_tyreStintsHistoryData[j].m_tyreVisualCompound != VisualTyreCompound.None)
                                    {
                                        newestTyreIndex = j;
                                        break;
                                    }
                                }


                                var counter = 0;
                                for (int j = newestTyreIndex; j >= 0; j--)
                                {
                                    Model.RelativeInfoArr[count].TyreTypes[counter] = packet.m_tyreStintsHistoryData[j].m_tyreVisualCompound;
                                    counter++;
                                }
                                // Set the rest as nothing
                                for(int j = counter; j < 8; j++)
                                {
                                    Model.RelativeInfoArr[count].TyreTypes[j] = VisualTyreCompound.None;
                                }
                   
      

                            }
                           

                            Model.RelativeInfoArr[count].TyreWear = highestWear;
                            Model.RelativeInfoArr[count].TyreAge = tyreAge;
                            Model.RelativeInfoArr[count].CurrentLapTime = currentLapTime;
                            Model.RelativeInfoArr[count].LastLapTime = lastLapTime;
                            Model.RelativeInfoArr[count].S1Time = s1Time;
                            Model.RelativeInfoArr[count].S2Time = s2Time;
                            Model.RelativeInfoArr[count].S3Time = s3Time;
                            Model.RelativeInfoArr[count].FastestLapTime = fastestLapTime;

                            count++;

                        }

                    }));
            }
        }
    }
}
