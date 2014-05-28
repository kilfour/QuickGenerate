using System;
using System.Security.Cryptography;

namespace QuickGenerate.Implementation
{
    public static class Seed
    {
        public static readonly MoreRandom Random = new MoreRandom();
    }

    public class MoreRandom
    {
        public int Next(int minimumValue, int maximumValue)
        {
            if (maximumValue <= minimumValue)
                return minimumValue;

            var randomNumber = new byte[1];

            var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);

            double value1 = (Convert.ToDouble(randomNumber[0]) / 255d);
            double value2 = Math.Round(value1 * (maximumValue - minimumValue - 1));

            return (int)(minimumValue + value2);
        }

        public double NextDouble()
        {
            var randomNumber = new byte[1];

            var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(randomNumber);
            return (Convert.ToDouble(randomNumber[0]) / 256d);
        }
    }
}