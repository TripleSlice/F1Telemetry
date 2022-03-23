using F1T.MVVM.ViewModels;
using F1T.Settings;
using F1T.Structs;
using F1T.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace F1T.MVVM.Views.Radar
{
    /// <summary>
    /// Interaction logic for RadarOverlayView.xaml
    /// </summary>
    public partial class RadarOverlayView : BaseOverlayView<RadarViewModel, RadarSettings>
    {
        // === ViewModel ===
        public override RadarViewModel Model { get => RadarViewModel.GetInstance(); }

        SolidColorBrush NiceBlue = new SolidColorBrush(Color.FromRgb(3, 144, 252));
        SolidColorBrush NiceRed = new SolidColorBrush(Color.FromRgb(247, 68, 45));
        SolidColorBrush NiceYellow = new SolidColorBrush(Color.FromRgb(252, 211, 3));

        List<Rectangle> Rectangles = new List<Rectangle>();

        public RadarOverlayView()
        {      
            this.DataContext = Model;
            InitializeComponent();
            UpdateValues();

            StartTimer();
        }

        private bool isInsideSquare(double X, double Y, int radius)
        {
            return Math.Abs(X) < (Model.CarWidth / 2) + radius  && Math.Abs(Y) < (Model.CarHeight / 2) + radius * 2f;
        }

        private void ClearRectanglesFromCanvas()
        {
            for (int i = Rectangles.Count - 1; i >= 0; --i)
            {
                CanvasInstance.Children.Remove(Rectangles[i]);
                // Old and laggy
                // Rectangles.RemoveAt(i);
            }
            // Better
            Rectangles.Clear();
        }

        protected override void UpdateValues(object state = null)
        {
            UpdateTimer();

            if (Model.OverlayVisible && Model.PlayerIndex != -1)
            {
                // Must start this here, or else we could be dealing with de-sync between values of Model and this code...
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        // Clear the rectangles from the previous call of this func
                        ClearRectanglesFromCanvas();

                        // Set the PlayerCar and AllCars variables
                        CarMotionData PlayerCar = Model.PlayerCarMotionData;
                        CarMotionData[] AllCars = Model.MotionData.m_carMotionData;
                        LapData[] AllLapData = Model.LapData.m_lapData;
                        int PlayerCarIndex = Model.PlayerIndex;

                        CarMotionData[] carsCloseToPlayer = CarMotionUtils.GetCarsInRadius(AllCars, AllLapData, PlayerCarIndex, 20);

                        foreach(var car in carsCloseToPlayer)
                        {
                            var deltaX = car.m_worldPositionX - PlayerCar.m_worldPositionX;
                            var deltaZ = car.m_worldPositionZ - PlayerCar.m_worldPositionZ;
                            double deltaYawRad = car.m_yaw - PlayerCar.m_yaw;
                            double deltaYaw = (180 / Math.PI) * deltaYawRad; // Degrees

                            // Polar coordinates
                            var radius = Math.Sqrt(deltaX * deltaX + deltaZ * deltaZ);
                            var phi1 = Math.Atan2(deltaZ, deltaX);
                            var phi2 = -PlayerCar.m_yaw;

                            var deltaPhi = phi1 - phi2;

                            // Convert from polar back to cartesian
                            var X = -(radius * Math.Cos(deltaPhi)) * Model.Scale;
                            var Y = -(radius * Math.Sin(deltaPhi)) * Model.Scale;

                            // default brush
                            SolidColorBrush color = NiceBlue;
                            if (isInsideSquare(X, Y, Model.DangerRadius)) color = NiceRed;
                            else if (isInsideSquare(X, Y, Model.WarningRadius)) color = NiceYellow;


                            Rectangle rec = new Rectangle()
                            {
                                Width = Model.CarWidth,
                                Height = Model.CarHeight,
                                Fill = color,
                                RadiusY = 5,
                                RadiusX = 5
                            };

                            // Rotate the rectangle so that it displays the rotation
                            rec.RenderTransformOrigin = new Point(0.5, 0.5);
                            rec.RenderTransform = new RotateTransform(-deltaYaw); ;

                            CanvasInstance.Children.Add(rec);
                            Rectangles.Add(rec);

                            // Set our X and Y multiplied by our scale, subtracted by where our 0,0 is (NOT CANVAS 0,0)
                            Canvas.SetLeft(rec, Model.PlayerCarLeft + X);
                            Canvas.SetTop(rec, Model.PlayerCarTop + Y);
                        }
                    }));
            }
        }
    }
}

//For use the old way of rotating incase this one breaks for some reason...
//Canvas.RenderTransformOrigin = new Point(0.5, 0.5);
//PlayerCarRect.RenderTransformOrigin = new Point(0.5, 0.5);
//rec.RenderTransform = new RotateTransform(deltaYaw);
//Canvas.RenderTransform = new RotateTransform(playerYaw);
//PlayerCarRect.RenderTransform = new RotateTransform(-playerYaw);