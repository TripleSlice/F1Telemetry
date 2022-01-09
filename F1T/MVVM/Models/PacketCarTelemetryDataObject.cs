using F1T.Core;
using F1T.Structs;

namespace F1T.MVVM.Models
{
    public class PacketCarTelemetryDataObject : ObservableObject
    {

        private CarTelemetryDataObject[] _m_carTelemetryData;
        public CarTelemetryDataObject[] m_carTelemetryData // Speed of car in kilometres per hour
        {
            get { return _m_carTelemetryData; }
            set { SetField(ref _m_carTelemetryData, value, "m_carTelemetryData"); }
        }

        public byte m_mfdPanelIndex;
        public byte m_mfdPanelIndexSecondaryPlayer;
        public sbyte m_suggestedGear;

        public PacketCarTelemetryDataObject()
        {
            m_mfdPanelIndex = 0;
            m_mfdPanelIndexSecondaryPlayer = 0;
            m_suggestedGear = 0;
        }

        public PacketCarTelemetryDataObject(PacketCarTelemetryData data)
        {
            m_carTelemetryData = CarTelemetryDataObject.FromArray(data.m_carTelemetryData);
            m_mfdPanelIndex = data.m_mfdPanelIndex;
            m_mfdPanelIndexSecondaryPlayer = data.m_mfdPanelIndexSecondaryPlayer;
            m_suggestedGear = data.m_suggestedGear;
        }

    }
}
