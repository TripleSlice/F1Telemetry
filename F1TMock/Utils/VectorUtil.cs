using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.Utils
{
    internal class VectorUtil
    {
        [Obsolete]
        public static void Increment(ref Vector3 vec, float x, float y, float z)
        {
            vec.X += x;
            vec.Y += y;
            vec.Z += z;
        }

        public static void Increment(ref Vector3 vec, Intensity intensity)
        {
            switch (intensity)
            {
                case Intensity.Low:
                    vec.X += 0.001f;
                    vec.Y += 0.002f;
                    vec.Z += 0.001f;
                    break;
                case Intensity.Medium:
                    vec.X += 0.004f;
                    vec.Y += 0.002f;
                    vec.Z += 0.004f;
                    break;
                case Intensity.High:
                    vec.X += 0.008f;
                    vec.Y += 0.0010f;
                    vec.Z += 0.008f;
                    break;
                default:
                    vec.X += 0.001f;
                    vec.Y += 0.002f;
                    vec.Z += 0.001f;
                    break;
            }
        }
    }
}
