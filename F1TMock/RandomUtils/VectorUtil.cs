using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.RandomUtils
{
    internal class VectorUtil
    {
        public static void Increment(ref Vector3 vec, float x, float y, float z)
        {
            vec.X += x;
            vec.Y += y;
            vec.Z += z;
        }
    }
}
