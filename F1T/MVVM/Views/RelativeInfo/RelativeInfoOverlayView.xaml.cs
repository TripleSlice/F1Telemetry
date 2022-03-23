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

            UpdateTimer();

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
                        PositionRange range = LapDataUtils.GetClosestNPositions(AllCarLapData, 2, PlayerCar.m_carPosition);

                        var count = 0;
                        for (int pos = range.Top; pos <= range.Bottom; pos++)
                        {
                            // Wear, Age, Laptime, Sector times
                            int highestWear = IndexingUtils.GetDataFromPosition(AllCarDamageData, IndexToPositionArr, pos).m_tyresDamage.Max();
                            int tyreAge = IndexingUtils.GetDataFromPosition(AllCarStatusData, IndexToPositionArr, pos).m_tyresAgeLaps;
                            uint currentLapTime = IndexingUtils.GetDataFromPosition(AllCarLapData, IndexToPositionArr, pos).m_currentLapTimeInMS;
                            uint lastLapTime = IndexingUtils.GetDataFromPosition(AllCarLapData, IndexToPositionArr, pos).m_lastLapTimeInMS;
                            ushort s1Time = IndexingUtils.GetDataFromPosition(AllCarLapData, IndexToPositionArr, pos).m_sector1TimeInMS;
                            ushort s2Time = IndexingUtils.GetDataFromPosition(AllCarLapData, IndexToPositionArr, pos).m_sector2TimeInMS;
                            ushort s3Time = (ushort)(currentLapTime - (s1Time + s2Time));
 
                            // Fastest Laptime, Visual Tyre Compounds
                            uint fastestLapTime = 0;
                            PacketSessionHistoryData packet;
                            var index = IndexingUtils.GetIndexFromPosition(IndexToPositionArr, pos);

                            // TODO abstract this aswell..
                            if (Model.SessionHistoryData.TryGetValue(index, out packet))
                            {
                                // prevent index out of bound
                                if (packet.m_lapHistoryData.Count() <= packet.m_bestLapTimeLapNum - 1) continue;

                                fastestLapTime = SessionHistoryUtils.GetFastestLapTime(packet);

                                
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
