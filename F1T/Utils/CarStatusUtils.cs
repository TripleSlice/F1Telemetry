using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F1T.Structs;

namespace F1T.Utils
{
    public static class CarStatusUtils
    {
        /// <summary>
        /// Returns a Dictonary where the key is <see cref="VisualTyreCompound"/> and the value is a <see cref="int"/> corresponding to the number of people on that specific <see cref="VisualTyreCompound"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Dictionary<VisualTyreCompound, int> GetActiveTyreCount(CarStatusData[] data)
        {
            return data.GroupBy(item => item.m_visualTyreCompound).ToDictionary(x => x.Key, x => x.Count());
        }
        /// <summary>
        /// Returns a <see cref="int"/> corresponding to the number of people on that specific <see cref="VisualTyreCompound"/>
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int CountDriverOnTyreType(CarStatusData[] data, VisualTyreCompound type)
        {
            return data.Count(item => item.m_visualTyreCompound == type);
        }
    }
}
