using F1T.Structs;
using System.Linq;

namespace F1T.Utils
{
    public static class SessionHistoryUtils
    {
        /// <summary>
        /// Gets the fastest lap in the <see cref="PacketSessionHistoryData"/>
        /// Returns 0 if no fastest lap found
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static uint GetFastestLapTime(PacketSessionHistoryData packet)
        {
            return packet.m_bestLapTimeLapNum >= 1 ?  packet.m_lapHistoryData[packet.m_bestLapTimeLapNum - 1].m_lapTimeInMS : 0;
        }
    }
}
