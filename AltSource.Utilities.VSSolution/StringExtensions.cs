using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltSource.Utilities.VSSolution
{
    public static class StringExtensions
    {

        /// <summary>
        /// Creates a relative path from one file or folder to another.
        /// </summary>
        /// <param name="fromPath">Contains the directory that defines the start of the relative path.</param>
        /// <param name="toPath">Contains the path that defines the endpoint of the relative path.</param>
        /// <returns>The relative path from the start directory to the end path.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="fromPath"/> or <paramref name="toPath"/> is <c>null</c>.</exception>
        /// <exception cref="UriFormatException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static string GetRelativePathTo(this string fromPath, string toPath)
        {
            if (string.IsNullOrEmpty(fromPath))
            {
                throw new ArgumentNullException("fromPath");
            }

            if (string.IsNullOrEmpty(toPath))
            {
                throw new ArgumentNullException("toPath");
            }

            var fromUri = new Uri(AppendDirectorySeparatorChar(fromPath));
            var toUri = new Uri(AppendDirectorySeparatorChar(toPath));

            if (fromUri.Scheme != toUri.Scheme)
            {
                return toPath;
            }

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (string.Equals(toUri.Scheme, Uri.UriSchemeFile, StringComparison.OrdinalIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }

        private static string AppendDirectorySeparatorChar(string path)
        {
            // Append a slash only if the path is a directory and does not have a slash.
            if (!Path.HasExtension(path) &&
                !path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                return path + Path.DirectorySeparatorChar;
            }

            return path;
        }


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

        public static string GetBetween(this string input, string start, string end)
        {
            int nada = 0;
            return GetBetween(input, start, end, ref nada);
        }


    }
}
