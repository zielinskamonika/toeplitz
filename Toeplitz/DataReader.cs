using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toeplitz
{
    class DataHandler
    {
        public static void ReadData(string filepath, out ToeplitzMatrix m, out long[] v)
        {
            var lines = File.ReadAllLines(filepath);
            if (lines.Length < 3)
                throw new InvalidDataException();
            long n = long.Parse(lines[0]);

            var a = lines[1].Split(' ');
            var a2 = a.Select(x => long.Parse(x)).ToArray();
            if (a.Length != 2 * n - 1)
                throw new InvalidDataException();
            a.Reverse();
            
            long[] vect = lines[2].Split(' ').Select(x => long.Parse(x)).ToArray();
            if (vect.Length != n)
                throw new InvalidDataException();

            m = new ToeplitzMatrix(a2);
            v = vect;
        }
        public static void SaveResult(string testFilepath, long[] v)
        {
            Directory.CreateDirectory(GetResultDirectory(testFilepath));

            string[] lines = new string[2];
            lines[0] = v.Length.ToString();
            lines[1] = "";

            foreach(long elem in v)
            {
                lines[1] += elem + " ";
            }

            File.WriteAllLines(GetResultFilepath(testFilepath), lines);
        }

        public static string GetResultFilepath(string testFilepath)
        {
            string directory = GetResultDirectory(testFilepath);
            return Path.Combine(directory, Path.GetFileNameWithoutExtension(testFilepath) + "_result.txt");
        }

        public static string GetResultDirectory(string testFilepath)
        {
            string directory = Path.GetDirectoryName(testFilepath);

            if (string.IsNullOrWhiteSpace(directory))
                directory = Directory.GetCurrentDirectory();

            return Path.Combine(Path.GetDirectoryName(testFilepath), "Results");
        }
    }
}
