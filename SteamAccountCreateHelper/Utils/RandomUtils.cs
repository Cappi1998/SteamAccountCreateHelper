namespace Steam_ACC_Create
{
    using System;

    class RandomUtils
    {
        private static Random r = new Random();

        public static int GetRandomInt(int minBound, int maxBound)
        {
            return r.Next(minBound, maxBound);
        }
    }
}