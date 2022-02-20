using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.Utils
{
    public static class IndexingUtils
    {
        public static T GetByRealPosition<T>(T[] data, int[] indexArr, int position)
        {
            return data[indexArr[position - 1]];
        }

        public static int GetRealIndex(int[] indexArr, int position)
        {
            return indexArr[position - 1];
        }
    }
}
