using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.Utils
{
    public static class IndexingUtils
    {
        /// <summary>
        /// Given a computed <see cref="int"/>[] indexArr and position, get the T data for that specific position
        /// <para>See <see cref="LapDataUtils.UpdatePositionArray(Structs.LapData[], ref int[])"/> to compute indexArr</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="indexArr"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static T GetDataFromPosition<T>(T[] data, int[] indexArr, int position)
        {
            return data[indexArr[position - 1]];
        }

        /// <summary>
        /// Give a computed indexArr and position, get the index of the item for that position
        /// <para>See <see cref="LapDataUtils.UpdatePositionArray(Structs.LapData[], ref int[])"/> to compute indexArr</para>
        /// </summary>
        /// <param name="indexArr"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static int GetIndexFromPosition(int[] indexArr, int position)
        {
            return indexArr[position - 1];
        }
    }
}
