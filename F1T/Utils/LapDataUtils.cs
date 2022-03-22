using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.Utils
{
    public static class LapDataUtils
    {
        /// <summary>
        /// Get how many cars are left in the race
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int GetActiveCarCount(LapData[] data)
        {
            // Count how many active cars there are
            int carCount = 22;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].m_resultStatus == ResultStatus.Invalid || data[i].m_resultStatus == ResultStatus.Inactive)
                {
                    carCount -= 1;
                    continue;
                }
            }
            return carCount;
        }

        /// <summary>
        /// Translates the <see cref="LapData"/> array into a int[] with indexs which point to the position in the UDP arrays
        /// <para> Example [2,4,3,0,1] means LapData[2] is in first, LapData[4] is in second, ... LapData[1] is in last... etc</para>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="indexToPosition"></param>
        /// <returns></returns>
        public static void UpdatePositionArray(LapData[] data, ref int[] indexToPosition)
        {
            // Sort the cars in the order that they are on track
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].m_resultStatus == ResultStatus.Invalid || data[i].m_resultStatus == ResultStatus.Inactive) continue;

                var trueIndex = data[i].m_carPosition - 1;
                indexToPosition[trueIndex] = i;
            }
        }
    }
}
