using F1T.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1T.Utils
{
    public static class CarMotionUtils
    {
        /// <summary>
        /// Returns an array of <see cref="CarMotionData"/> of cars around the car at the specified index, not including the car at the specified index
        /// </summary>
        /// <param name="data"></param>
        /// <param name="playerIndex"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static CarMotionData[] GetCarsInRadius(CarMotionData[] data, LapData[] lapData, int playerIndex, int radius)
        {
            CarMotionData playerCar = data[playerIndex];
            int[] activeCars = LapDataUtils.GetActiveCarIndexs(lapData);

            return data.Select((car, index) => (car, index))
                .Where((item) =>
                    IsWithinRadius(playerCar, item.car, radius) &&
                    !item.car.Equals(playerCar) &&
                    activeCars.Contains(item.index))
                .Select(item => item.car) 
                .ToArray();
        }
        /// <summary>
        /// Helper function which returns boolean if the car is within "radius" of other car
        /// radius is in quotes since it is a square around the car
        /// </summary>
        /// <param name="playerCar"></param>
        /// <param name="otherCar"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private static Boolean IsWithinRadius(CarMotionData playerCar, CarMotionData otherCar, int radius)
        {
            return Math.Abs(otherCar.m_worldPositionX - playerCar.m_worldPositionX) < radius && Math.Abs(otherCar.m_worldPositionZ - playerCar.m_worldPositionZ) < radius;
        }
    }
}
