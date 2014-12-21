﻿using System;
using System.Security.Cryptography;

namespace OnlineTabletop.Util
{
    /// <summary>
    /// Utility class for simulating rolling of dice.
    /// </summary>
    /// <remarks>
    /// Usage:
    /// <example>
    /// var attackRoll = Roll.d20() + character.BAB + character.Strength; 
    /// var damage = Roll.d4(2) + 1;
    /// </example>
    /// </remarks>
    public static class Roll
    {
        static Roll()
        {
            Rng = CreateDefaultRng();
        }

        /// <summary>
        /// Gets or sets the random number generator used to simulate rolls.
        /// </summary>
        public static Random Rng { get; set; }

        /// <summary>
        /// Creates the default RNG implementation used by this class.
        /// </summary>
        /// <returns>A random number generator.</returns>
        public static Random CreateDefaultRng()
        {
            // Initialize with a true random seed, instead of the current time.
            var secureRng = new RNGCryptoServiceProvider();
            byte[] seed = new byte[4];
            secureRng.GetBytes(seed);
            return new Random(BitConverter.ToInt32(seed, 0));
        }

        /// <summary>
        /// Gets the result of rolling a 100-sided die.
        /// </summary>
        public static int d100()
        {
            return GenerateResult(100, 1);
        }

        /// <summary>
        /// Gets the result of rolling a 20-sided die.
        /// </summary>
        public static int d20()
        {
            return GenerateResult(20, 1);
        }

        /// <summary>
        /// Gets the result of rolling a 10-sided die.
        /// </summary>
        public static int d10()
        {
            return GenerateResult(10, 1);
        }

        /// <summary>
        /// Gets the combined result of rolling N 10-sided dice.
        /// </summary>
        public static int d10(int n)
        {
            return GenerateResult(10, n);
        }

        /// <summary>
        /// Gets the result of rolling an 8-sided die.
        /// </summary>
        public static int d8()
        {
            return GenerateResult(8, 1);
        }

        /// <summary>
        /// Gets the combined result of rolling N 8-sided dice.
        /// </summary>
        public static int d8(int n)
        {
            return GenerateResult(8, n);
        }

        /// <summary>
        /// Gets the result of rolling a 6-sided die.
        /// </summary>
        public static int d6()
        {
            return GenerateResult(6, 1);
        }

        /// <summary>
        /// Gets the combined result of rolling N 6-sided dice.
        /// </summary>
        public static int d6(int n)
        {
            return GenerateResult(6, n);
        }

        /// <summary>
        /// Gets the result of rolling a 4-sided die.
        /// </summary>
        public static int d4()
        {
            return GenerateResult(4, 1);
        }

        /// <summary>
        /// Gets the combined result of rolling N 4-sided dice.
        /// </summary>
        public static int d4(int n)
        {
            return GenerateResult(4, n);
        }

        private static int GenerateResult(int sides, int numberOfDice)
        {
            int total = 0;
            for (int i = 0; i < numberOfDice; i++)
            {
                total += Rng.Next(sides) + 1;
            }

            return total;
        }
    }
}