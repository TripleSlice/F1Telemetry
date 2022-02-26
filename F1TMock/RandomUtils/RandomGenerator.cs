using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1TMock.RandomUtils
{
    internal static class RandomGenerator
    {
        private static Random _random = new Random();
        public static int GenerateRandomInt(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
