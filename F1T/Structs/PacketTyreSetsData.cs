using System.Runtime.InteropServices;

namespace F1T.Structs
{

    public struct TyreSetData
    {
        public ActualTyreCompund m_actualTyreCompound;    // Actual tyre compound used
        public VisualTyreCompound m_visualTyreCompound;    // Visual tyre compound used
        public byte m_wear;                  // Tyre wear (percentage)
        public byte m_available;             // Whether this set is currently available
        public byte m_recommendedSession;    // Recommended session for tyre set
        public byte m_lifeSpan;              // Laps left in this tyre set
        public byte m_usableLife;            // Max number of laps recommended for this compound
        public short m_lapDeltaTime;          // Lap delta time in milliseconds compared to fitted set
        public byte m_fitted;                // Whether the set is fitted or not
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PacketTyreSetsData
    {
        public PacketHeader m_header;                  // Header

        public byte m_carIdx;            // Index of the car this data relates to
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        TyreSetData[] m_tyreSetData;	// 13 (dry) + 7 (wet)
        public byte m_fittedIdx;         // Index into array of fitted tyre
    }

}
