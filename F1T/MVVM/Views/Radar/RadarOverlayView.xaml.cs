using F1T.MVVM.ViewModels;
using F1T.Structs;
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
    public partial class RadarOverlayView : BaseOverlayView<RadarViewModel>
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


        protected override void UpdateValues(object state = null)
        {

            if (Model.OverlayVisible && Model.PlayerIndex != -1)
            {
                // Must start this here, or else we could be dealing with de-sync between values of Model and this code...
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        // Clear the rectangles from the previous call of this func
                        for (int i = Rectangles.Count - 1; i >= 0; --i)
                        {
                            CanvasInstance.Children.Remove(Rectangles[i]);
                            //Rectangles.RemoveAt(i);
                        }
                        Rectangles.Clear();

                        // Set the PlayerCar and AllCars variables
                        CarMotionData PlayerCar = Model.PlayerCarMotionData;
                        CarMotionData[] AllCars = Model.MotionData.m_carMotionData;
                        int PlayerCarIndex = Model.PlayerIndex;

                        for (int i = 0; i < AllCars.Length; i++)
                        {
                            // If we are on the player car...
                            if (i == PlayerCarIndex)
                            {
                                continue;
                            }

                            // Get the current car object
                            CarMotionData Car = AllCars[i];

                            // Not sure why but sometimes the carYaw == 0 (ghost cars?)
                            if (Car.m_yaw == 0)
                            {
                                continue;
                            }


                            // https://gamedev.stackexchange.com/questions/79765/how-do-i-convert-from-the-global-coordinate-space-to-a-local-space
                            // https://gamedev.stackexchange.com/questions/198852/converting-from-global-coordinates-to-local-coordinates/198860#198860
                            // Compute delta between two cars
                            var deltaX = Car.m_worldPositionX - PlayerCar.m_worldPositionX;
                            var deltaZ = Car.m_worldPositionZ - PlayerCar.m_worldPositionZ;

                            // Arbitrary square selected as 20 world units out in every direction
                            // aka a 40 x 40 square
                            if (Math.Abs(deltaX) < 20 && Math.Abs(deltaZ) < 20)
                            {
                                // If we are within this square, perform calculations
                                double deltaYawRad = Car.m_yaw - PlayerCar.m_yaw;
                                double deltaYaw = (180 / Math.PI) * deltaYawRad;

                                // Polar coordinates
                                var radius = Math.Sqrt(deltaX * deltaX + deltaZ * deltaZ);
                                var phi1 = Math.Atan2(deltaZ, deltaX);
                                var phi2 = -PlayerCar.m_yaw;

                                var deltaPhi = phi1 - phi2;

                                // Convert from polar back to cartesian
                                var X = -(radius * Math.Cos(deltaPhi)) * Model.Scale;
                                var Y = -(radius * Math.Sin(deltaPhi)) * Model.Scale;

                                SolidColorBrush color = NiceBlue;

                                if (isInsideSquare(X, Y, Model.DangerRadius))
                                {
                                    color = NiceRed;
                                }else if (isInsideSquare(X, Y, Model.WarningRadius))
                                {
                                    color = NiceYellow;
                                }

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