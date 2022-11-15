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
        /// Returns the amount of cars that are still active in the race
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int GetActiveCarCount(LapData[] data)
        {
            return data.Count(car => car.m_resultStatus != ResultStatus.Invalid || car.m_resultStatus != ResultStatus.Inactive);
        }

        /// <summary>
        /// Returns a <see cref="int"/>[] of index of cars that are active
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int[] GetActiveCarIndexs(LapData[] data)
        {
            return data.Where(car => car.m_resultStatus != ResultStatus.Invalid || car.m_resultStatus != ResultStatus.Inactive)
                .Select((car, index) => index)
                .ToArray();
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
            // Create a array where we can look up the index of an item based on its position
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].m_resultStatus == ResultStatus.Invalid || data[i].m_resultStatus == ResultStatus.Inactive) continue;

                var trueIndex = data[i].m_carPosition - 1;
                indexToPosition[trueIndex] = i;
            }

            // Update to something like this
            /*
             
                int[] lastLapTimeLookupArray = lapData.m_lapData
                    .Select((data, index) => new { data, index }) // Create a object that looks like {LapData data, int index}
                    .OrderBy(value => value.data.m_lastLapTimeInMS) // Order it by the m_lastLapTimeInMS property
                    .Select(value => value.index) // Take only the index property from the sorted array
                    .ToArray(); // Convert the array
             */
        }

        /// <summary>
        /// Returns positions ± n to the player, and the player. Will always return n * 2 + 1 positions.
        /// This means if you are last, it will return yourself, and n * 2 people infront of you
        /// </summary>
        /// <param name="n"></param>
        /// <param name="playerIndex"></param>
        public static PositionRange GetClosestNPositions(LapData[] data, int n, int playerPosition)
        {
            var carCount = GetActiveCarCount(data);

            int topPosition = playerPosition - n;
            int bottomPosition = playerPosition + n;

            if (topPosition < 1)
            {
                topPosition = 1;
                bottomPosition = topPosition + (2 * n);
            }
            else if (bottomPosition > carCount)
            {
                topPosition = carCount - (2 * n);
                bottomPosition = topPosition + (2 * n);
            }

            if (carCount < bottomPosition) bottomPosition = carCount;

            return new PositionRange(topPosition, bottomPosition);
        }
    }

    public class PositionRange
    {
        public PositionRange(int top, int bottom){
            Top = top;
            Bottom = bottom;
        }
        public int Top;
        public int Bottom;
    }
}
