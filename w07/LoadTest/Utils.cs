using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LoadTest
{
    public static class Utils
    {
        public static double GetMedian(this float[] source)
        {
            var count = source.Length;

            if (count == 0)
            {
                return 0;
            }

            if (count % 2 != 0)
            {
                return source[count / 2];
            }

            var a = source[count / 2 - 1];
            var b = source[count / 2];
            return (a + b) / 2;
        }

        public static double GetStdDev(this float[] source)
        {
            if (source.Length <= 1)
            {
                return -1;
            }

            var avg = source.Average();
            var sum = source.Sum(d => Math.Pow(d - avg, 2));
            return Math.Sqrt(sum / (source.Length - 1));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source">sorted array</param>
        /// <param name="nth"></param>
        /// <returns></returns>
        public static float GetPercentile(this float[] source, int nth)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            var idx = (int)(Math.Ceiling(source.Length * nth / 100.0));

            return source[idx - 1];
        }

        public static IEnumerable<string> OutputStatusCode(Dictionary<int, int> status)
        {
            foreach ((int k, int v) in status)
            {
                yield return $"\t{k}: {v}";
            }
        }

        /// <summary>
        /// Timer Tick to Milliseconds
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public static float TickToMilliseconds(long tick)
        {
            return tick * 1.0f / Stopwatch.Frequency * 1000;
        }
    }
}