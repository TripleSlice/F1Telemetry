﻿using F1T.DataStructures;
using F1T.MVVM.ViewModels;
using F1T.Settings;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace F1T.MVVM.Views.InputTelemetry
{

    public partial class InputTelemetryOverlayView : BaseOverlayView<InputTelemetryViewModel, InputTelemetrySettings>
    {
        private static int dummyInput = 50;
        private FixedSizeQueue<double> ThrottleValues = new FixedSizeQueue<double>(dummyInput);
        private FixedSizeQueue<double> BrakeValues = new FixedSizeQueue<double>(dummyInput);
        private FixedSizeQueue<double> GearValues = new FixedSizeQueue<double>(dummyInput);

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
            InputTelemetryPlot.RightClicked -= InputTelemetryPlot.DefaultRightClickEvent;

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
            GearValues = new FixedSizeQueue<double>(calculatedArraySize);

            InputTelemetryPlot.Plot.Clear();


            // Lower has priority over everything
            // Throttle will display overtop of gear... etc
            InputTelemetryPlot.Plot.PlotSignal(GearValues.ToArray(), 1, 0, 0, System.Drawing.Color.Gray, 3);
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
            UpdateTimer();

            if (Model.OverlayVisible && Model.PlayerIndex != -1)
            {
                if (Model.Settings.BrakeChartVisible) { BrakeValues.Push(Model.PlayerCarTelemetryData.m_brake); }
                else { BrakeValues.Push(-1); }

                if (Model.Settings.ThrottleChartVisible) { ThrottleValues.Push(Model.PlayerCarTelemetryData.m_throttle); }
                else { ThrottleValues.Push(-1); }

                if (Model.Settings.GearChartVisible) { GearValues.Push(Model.PlayerCarTelemetryData.m_gear / 8f); }
                else { GearValues.Push(-1); }
                
                
                Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => {
                    InputTelemetryPlot.Render();
                }));
            }
        }

        private void InputTelemetryPlot_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
