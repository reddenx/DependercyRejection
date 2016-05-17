using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class StringExtensions
    {
        /// <summary>
        /// reminder to self, takes index used as start point and outputs as ending point
        /// </summary>
        public static string GetBetween(this string input, string start, string end, ref int index)
        {
            var firstInstanceOfStart = input.IndexOf(start, index);
            var firstInstanceOfEnd = input.IndexOf(end, firstInstanceOfStart + end.Length);

            if (firstInstanceOfStart > 0 && firstInstanceOfEnd > 0)
            {
                var startCapture = firstInstanceOfStart + start.Length;
                var capture = input.Substring(startCapture, firstInstanceOfEnd - startCapture);
                index = firstInstanceOfEnd + end.Length;
                return capture;
            }

            index = input.Length;
            return null;
        }
    }
}
