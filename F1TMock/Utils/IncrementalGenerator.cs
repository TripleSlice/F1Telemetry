namespace F1TMock.Utils
{
    /// <summary>
    /// Generates numbers that constantly loop around
    /// </summary>
    public static class IncrementalGenerator
    {

        private static Dictionary<string, double> _doubleMap = new Dictionary<string, double>();

        public static double GetDoubleNumber(string key, double min, double max, double increment)
        {
            double num = _doubleMap.GetValueOrDefault(key);
            num += increment;
            if(num < min || num > max) num = min;
            _doubleMap[key] = num;
            return num;
        }

        public static float GetFloatNumber(string key, float min, float max, float increment)
        {
            double num = _doubleMap.GetValueOrDefault(key);
            num += increment;
            if (num < min || num > max) num = min;
            _doubleMap[key] = num;
            return (float)num;
        }

        public static int GetIntNumber(string key, int min, int max, int increment)
        {
            double num = _doubleMap.GetValueOrDefault(key);
            num += increment;
            if (num < min || num > max) num = min;
            _doubleMap[key] = num;
            return (int)num;
        }

        public static float[] GenerateFloatArray(string key, float min, float max, float increment, int length)
        {
            float[] arr = new float[length];
            float val = GetFloatNumber(key, min, max, increment);
            for (int i = 0; i < length; i++)
            {
                arr[i] = val;
            }
            return arr;
        }

        public static int[] GenerateIntArray(string key, int min, int max, int increment, int length)
        {
            int[] arr = new int[length];
            int val = GetIntNumber(key, min, max, increment);
            for (int i = 0; i < length; i++)
            {
                arr[i] = val;
            }
            return arr;
        }


        public static byte[] GenerateByteArray(string key, int min, int max, int increment, int length)
        {
            byte[] result = new byte[length];
            var arr = GenerateIntArray(key, min, max, increment, length);
            Buffer.BlockCopy(arr, 0, result, 0, length);
            return result;
        }

        public static sbyte[] GenerateSByteArray(string key, int min, int max, int increment, int length)
        {
            sbyte[] result = new sbyte[length];
            var arr = GenerateIntArray(key, min, max, increment, length);
            Buffer.BlockCopy(arr, 0, result, 0, length);
            return result;
        }

        public static ushort[] GenerateUShortArray(string key, int min, int max, int increment, int length)
        {
            ushort[] result = new ushort[length];
            var arr = GenerateIntArray(key, min, max, increment, length);
            Buffer.BlockCopy(arr, 0, result, 0, length);
            return result;
        }
    }
}
