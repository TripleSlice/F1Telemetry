using F1T.MVVM.Models;
using F1T.MVVM.ViewModels;
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
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections;
using System.Numerics;

namespace F1T.MVVM.Views.Radar
{
    /// <summary>
    /// Interaction logic for RadarOverlayView.xaml
    /// </summary>
    public partial class RadarOverlayView : Window, IOverlayView
    {

        private static int _timeBetweenLooks = 33;

        
        // === ViewModel ===
        public BaseModuleViewModel Model { get => RadarViewModel.GetInstance(); }
        public RadarViewModel RadarModel = RadarViewModel.GetInstance();


        SolidColorBrush NiceBlue = new SolidColorBrush(Color.FromRgb(3, 144, 252));

        List<Rectangle> Rectangles = new List<Rectangle>();

        public RadarOverlayView()
        {
            InitializeComponent();
            this.DataContext = RadarModel;

            UpdateValues();

            InitTimer();
        }


        private Timer timer;
        public void InitTimer()
        {
            timer = new Timer(UpdateValues, null, 0, _timeBetweenLooks);
        }


        public void UpdateValues(object state = null)
        {


            if (Model.OverlayVisible && Model.PacketViewModel.PlayerCarMotionData != null)
            {

                CarMotionDataObject PlayerCar = Model.PacketViewModel.PlayerCarMotionData;

                var worldForwardDirX = PlayerCar.m_worldForwardDirX / 32767.0f;
                var worldForwardDirZ = PlayerCar.m_worldForwardDirZ / 32767.0f;
                var worldRightDirX = PlayerCar.m_worldRightDirX / 32767.0f;
                var worldRightDirZ = PlayerCar.m_worldRightDirZ / 32767.0f;

                Vector2 worldForward = new Vector2(worldForwardDirX, worldForwardDirZ);
                Vector2 worldRight = new Vector2(worldRightDirX, worldRightDirZ);

                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        for (int i = Rectangles.Count - 1; i >= 0; --i)
                        {
                            Canvas.Children.Remove(Rectangles[i]);
                            //Rectangles.RemoveAt(i);
                        }
                        Rectangles.Clear();
                    }));




                for (int i = 0; i < Model.PacketViewModel.AllCarMotionData.m_carMotionData.Length; i++)
                {

                    CarMotionDataObject car = Model.PacketViewModel.AllCarMotionData.m_carMotionData[i];

                    var deltaX = PlayerCar.m_worldPositionX - car.m_worldPositionX;
                    var deltaZ = PlayerCar.m_worldPositionZ - car.m_worldPositionZ;

                    Vector2 deltaVector = new Vector2(deltaX, deltaZ);

                    var deltaYawRad = PlayerCar.m_yaw - car.m_yaw;
                    double deltaYaw = (180 / Math.PI) * deltaYawRad;


                    // If we are on the players car
                    if (i == Model.PacketViewModel.PlayerCarMotionIndex)
                    {
                        continue;
                    }

                    // Arbitrrary "Radius" (Square) selected as 100
                    if (Math.Abs(deltaX) < 100 && Math.Abs(deltaZ) < 100)
                    {
                        Application.Current.Dispatcher.BeginInvoke(
                                    DispatcherPriority.Normal,
                                    new Action(() =>
                                    {
                                        Rectangle rec = new Rectangle()
                                        {
                                            Width = RadarModel.CarWidth,
                                            Height = RadarModel.CarHeight,
                                            Fill = NiceBlue,
                                            RadiusY = 5,
                                            RadiusX = 5
                                        };

                                        rec.RenderTransformOrigin = new Point(0.5, 0.5);
                                        rec.RenderTransform = new RotateTransform(-deltaYaw);

                                        Canvas.Children.Add(rec);
                                        Rectangles.Add(rec);



                                        Console.WriteLine("worldForwardDirX: " + worldForwardDirX);
                                        Console.WriteLine("worldForwardDirZ: " + worldForwardDirZ);
                                        Console.WriteLine("worldRightDirX: " + worldRightDirX);
                                        Console.WriteLine("worldRightDirZ: " + worldRightDirZ);


                                        // values on straight
                                        // worldForwardDirX: -0.5568407
                                        // worldForwardDirZ: -0.001739555
                                        // worldRightDirX: -0.9992371
                                        // worldRightDirZ: 0.01614429

                                        // Same direction as straight
                                        // worldForwardDirX: -0.4811853
                                        // worldForwardDirZ: 0.02578814
                                        // worldRightDirX: -0.9893185
                                        // worldRightDirZ: 0.02691733


                                        // Opposite from straight
                                        // worldForwardDirX: 0.52031 <- switches
                                        // worldForwardDirZ: -0.0004272591
                                        // worldRightDirX: 0.9994202 <- switches
                                        // worldRightDirZ: 0.02755821


                                        // perpendicular to striaght
                                        // worldForwardDirX: -0.4921415
                                        // worldForwardDirZ: 0.01965392
                                        // worldRightDirX: -0.2396008 <- switches
                                        // worldRightDirZ: -0.01391644 <- switches


                                        // what "worked" before on straight
                                        //var moveUp = deltaZ * 1 + 0;
                                        //var moveLeft = deltaX * -1 + 0;
                                        // OR
                                        //var moveUp = deltaZ * -1 + 0;
                                        //var moveLeft = deltaX * -1 + 0;


                                        var moveUp = Vector2.Dot(deltaVector, worldForward);
                                        var moveLeft = -Vector2.Dot(deltaVector, worldRight);

                                        Canvas.SetTop(rec, RadarModel.PlayerCarTop + moveUp * RadarModel.Scale);
                                        Canvas.SetLeft(rec, RadarModel.PlayerCarLeft + moveLeft * RadarModel.Scale);

                                        // what "worked" before
                                        //Canvas.SetTop(rec, RadarModel.PlayerCarTop + deltaZ * RadarModel.Scale * -1);
                                        //Canvas.SetLeft(rec, RadarModel.PlayerCarLeft + deltaX * RadarModel.Scale * -1);

                                    }));
                    }
                }
            }
        }

        public void OnWindow_MouseDown(object sender, MouseButtonEventArgs e) { }
    }
}
