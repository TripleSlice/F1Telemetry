using F1T.DataStructures;
using F1T.MVVM.ViewModels;
using ScottPlot.Plottable;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace F1T.MVVM.Views.InputTelemetry
{

    public partial class InputTelemetryOverlayView : BaseOverlayView<InputTelemetryViewModel>
    {

        private static int _timeBetweenLooks = 50;
        private static int _sizeFor5Seconds = 7000 / _timeBetweenLooks;

        private FixedSizeQueue<double> ThrottleValues = new FixedSizeQueue<double>(_sizeFor5Seconds);
        private FixedSizeQueue<double> BrakeValues = new FixedSizeQueue<double>(_sizeFor5Seconds);
        private readonly Stopwatch Stopwatch = Stopwatch.StartNew();

        // === ViewModel ===
        public override InputTelemetryViewModel Model { get => InputTelemetryViewModel.GetInstance(); }


        public InputTelemetryOverlayView()
        {
            InitializeComponent();
            this.DataContext = Model;

            UpdateValues();

            // InputTelemetryPlot Styling and Init Plotting
            InputTelemetryPlot.Plot.PlotSignal(ThrottleValues.ToArray(), 1, 0, 0, System.Drawing.Color.LimeGreen, 3);
            InputTelemetryPlot.Plot.PlotSignal(BrakeValues.ToArray(), 1, 0, 0, System.Drawing.Color.Red, 3);
            InputTelemetryPlot.Plot.SetAxisLimits(0, _sizeFor5Seconds, -0.01, 1.01);
            InputTelemetryPlot.Plot.SetOuterViewLimits(0, _sizeFor5Seconds, -0.01, 1.01);
            InputTelemetryPlot.Plot.SetInnerViewLimits(0, _sizeFor5Seconds, -0.01, 1.01);
            InputTelemetryPlot.Plot.Style(figureBackground: System.Drawing.Color.Transparent);
            InputTelemetryPlot.Plot.Style(dataBackground: System.Drawing.Color.Transparent);
            double[] tempInt = new double[1] { -1 };
            string[] tempStr = new string[1] { "" };
            InputTelemetryPlot.Plot.XTicks(tempInt, tempStr);
            InputTelemetryPlot.Plot.YTicks(tempInt, tempStr);          
            InputTelemetryPlot.Plot.XLabel("");
            InputTelemetryPlot.Plot.YLabel("");
            InputTelemetryPlot.Plot.Grid(false);
            InputTelemetryPlot.Plot.Title("");
            InputTelemetryPlot.Plot.Frameless();
            InputTelemetryPlot.Render();

            InitTimer();
        }

        private Timer timer;
        public void InitTimer()
        {
            timer = new Timer(UpdateValues, null, 0, _timeBetweenLooks);
        }

        // Only update the values of the graph if
        // We have received PlayerCarTelemtryData and
        // If the Window is visible
        public void UpdateValues(object state = null)
        {
            if (Model.OverlayVisible && Model.PacketViewModel.PlayerCarTelemetryData != null)
            {
                BrakeValues.Push(Model.PacketViewModel.PlayerCarTelemetryData.m_brake);
                ThrottleValues.Push(Model.PacketViewModel.PlayerCarTelemetryData.m_throttle);

                Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => {
                    InputTelemetryPlot.Render();
                }));
            }
        }
        public void OnWindow_MouseDown(object sender, MouseButtonEventArgs e){}
    }
}
