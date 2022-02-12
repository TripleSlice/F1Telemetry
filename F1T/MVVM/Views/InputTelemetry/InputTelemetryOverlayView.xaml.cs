using F1T.Core;
using F1T.DataStructures;
using F1T.MVVM.ViewModels;
using F1T.Settings;
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

    public partial class InputTelemetryOverlayView : BaseOverlayView<InputTelemetryViewModel, InputTelemetrySettings>
    {
        private static int dummyInput = 50;
        private FixedSizeQueue<double> ThrottleValues = new FixedSizeQueue<double>(dummyInput);
        private FixedSizeQueue<double> BrakeValues = new FixedSizeQueue<double>(dummyInput);

        // === ViewModel ===
        public override InputTelemetryViewModel Model { get => InputTelemetryViewModel.GetInstance(); }


        public InputTelemetryOverlayView()
        {
            InitializeComponent();
            this.DataContext = Model;


            // This is here because if we do not call updatevalues atleast once
            UpdateValues();


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

            StartTimer();
        }


        public override void StartTimer()
        {
            // TODO move the 7000 to be configurable...
            // 7000 ms of data
            int calculatedArraySize = 7000 / Model.Settings.Frequency;

            ThrottleValues = new FixedSizeQueue<double>(calculatedArraySize);
            BrakeValues = new FixedSizeQueue<double>(calculatedArraySize);

            InputTelemetryPlot.Plot.Clear();

            InputTelemetryPlot.Plot.PlotSignal(ThrottleValues.ToArray(), 1, 0, 0, System.Drawing.Color.LimeGreen, 3);
            InputTelemetryPlot.Plot.PlotSignal(BrakeValues.ToArray(), 1, 0, 0, System.Drawing.Color.Red, 3);
            InputTelemetryPlot.Plot.SetAxisLimits(0, calculatedArraySize, -0.01, 1.01);
            InputTelemetryPlot.Plot.SetOuterViewLimits(0, calculatedArraySize, -0.01, 1.01);
            InputTelemetryPlot.Plot.SetInnerViewLimits(0, calculatedArraySize, -0.01, 1.01);
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

            // This is REALLY REALLY bad....
            // But the best way to do it
            // Issue was that Plot was not updating correctly
            // Delaying it, or throwing it in the UpdateValues loop (EVEN WORSE)
            // Seems to fix
            Thread.Sleep(1000);
            calculatedArraySize = 7000 / Model.Settings.Frequency;
            InputTelemetryPlot.Plot.SetAxisLimits(0, calculatedArraySize, -0.01, 1.01);
            InputTelemetryPlot.Plot.SetOuterViewLimits(0, calculatedArraySize, -0.01, 1.01);
            InputTelemetryPlot.Plot.SetInnerViewLimits(0, calculatedArraySize, -0.01, 1.01);

            timer = new Timer(UpdateValues, null, 0, Model.Settings.Frequency);
            currentFrequency = Model.Settings.Frequency;
        }

        // Only update the values of the graph if
        // We have received PlayerCarTelemtryData and
        // If the Window is visible

        protected override void UpdateValues(object state = null)
        {
            if (Model.Settings.Frequency != currentFrequency)
            {
                StopTimer();
                StartTimer();
            }


            if (Model.OverlayVisible && Model.PlayerIndex != -1)
            {
                BrakeValues.Push(Model.PlayerCarTelemetryData.m_brake);
                ThrottleValues.Push(Model.PlayerCarTelemetryData.m_throttle);

                Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => {
                    InputTelemetryPlot.Render();
                }));
            }
        }
    }
}
