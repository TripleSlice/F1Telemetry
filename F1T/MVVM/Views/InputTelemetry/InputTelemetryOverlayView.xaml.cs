using F1T.DataStructures;
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
        private const int MS_OF_DATA_VISIBLE = 7000;

        private FixedSizeQueue<double> ThrottleValues;
        private FixedSizeQueue<double> BrakeValues;
        private FixedSizeQueue<double> GearValues;

        // === ViewModel ===
        public override InputTelemetryViewModel Model { get => InputTelemetryViewModel.GetInstance(); }


        public InputTelemetryOverlayView()
        {
            InitializeComponent();
            this.DataContext = Model;

            InputTelemetryPlot.Plot.Style(figureBackground: System.Drawing.Color.Transparent);
            InputTelemetryPlot.Plot.Style(dataBackground: System.Drawing.Color.Transparent);
            InputTelemetryPlot.Plot.XTicks(new double[1] { -1 }, new string[1] { "" });
            InputTelemetryPlot.Plot.YTicks(new double[1] { -1 }, new string[1] { "" });          
            InputTelemetryPlot.Plot.XLabel("");
            InputTelemetryPlot.Plot.YLabel("");
            InputTelemetryPlot.Plot.Grid(false);
            InputTelemetryPlot.Plot.Title("");
            InputTelemetryPlot.Plot.Frameless();
            InputTelemetryPlot.RightClicked -= InputTelemetryPlot.DefaultRightClickEvent;

            InputTelemetryPlot.Render();

            StartTimer();
        }


        // Remake the graph
        public override void StartTimer()
        {
            int calculatedArraySize = MS_OF_DATA_VISIBLE / Model.Settings.Frequency;
            Console.WriteLine(Model.Settings.Frequency);

            ThrottleValues = new FixedSizeQueue<double>(calculatedArraySize);
            BrakeValues = new FixedSizeQueue<double>(calculatedArraySize);
            GearValues = new FixedSizeQueue<double>(calculatedArraySize);

            InputTelemetryPlot.Plot.Clear();

            // Lower has priority over everything
            // Throttle will display overtop of gear... etc
            var gearLine = InputTelemetryPlot.Plot.AddSignal(GearValues.ToArray(), 1, System.Drawing.Color.Gray);
            gearLine.LineWidth = 3;
            gearLine.MarkerSize = 0;
            var throttleLine = InputTelemetryPlot.Plot.AddSignal(ThrottleValues.ToArray(), 1, System.Drawing.Color.LimeGreen);
            throttleLine.LineWidth = 3;
            throttleLine.MarkerSize = 0;
            var breakLine = InputTelemetryPlot.Plot.AddSignal(BrakeValues.ToArray(), 1, System.Drawing.Color.Red);
            breakLine.LineWidth = 3;
            breakLine.MarkerSize = 0;

            InputTelemetryPlot.Plot.SetAxisLimits(0, calculatedArraySize, -0.01, 1.01);
            InputTelemetryPlot.Plot.SetOuterViewLimits(0, calculatedArraySize, -0.01, 1.01);
            InputTelemetryPlot.Plot.SetInnerViewLimits(0, calculatedArraySize, -0.01, 1.01);

            // This is REALLY REALLY bad.... But the best way to do it (I think...)
            // Issue was that InputTelemetryPlot was not updating correctly on refresh
            // Delaying it, or throwing it in the UpdateValues loop (EVEN WORSE) seems to fix the problem
            Thread.Sleep(1000);
            calculatedArraySize = MS_OF_DATA_VISIBLE / Model.Settings.Frequency;
            Console.WriteLine(Model.Settings.Frequency);
            InputTelemetryPlot.Plot.SetAxisLimits(0, calculatedArraySize, -0.01, 1.01);
            InputTelemetryPlot.Plot.SetOuterViewLimits(0, calculatedArraySize, -0.01, 1.01);
            InputTelemetryPlot.Plot.SetInnerViewLimits(0, calculatedArraySize, -0.01, 1.01);

            base.StartTimer();
        }

        protected override void UpdateValues(object state = null)
        {
            UpdateTimer();

            if (Model.OverlayVisible && Model.PlayerIndex != -1)
            {
                if (Model.Settings.BrakeChartVisible) BrakeValues.Push(Model.PlayerCarTelemetryData.m_brake); 
                else BrakeValues.Push(-1); 

                if (Model.Settings.ThrottleChartVisible) ThrottleValues.Push(Model.PlayerCarTelemetryData.m_throttle);
                else ThrottleValues.Push(-1);

                if (Model.Settings.GearChartVisible) GearValues.Push(Model.PlayerCarTelemetryData.m_gear / 8f);
                else GearValues.Push(-1);
                
                
                Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                new Action(() => {
                    InputTelemetryPlot.Render();
                }));
            }
        }

        // Disable double clicking on the chart
        private void InputTelemetryPlot_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}
