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
        public static void ReadData(string filepath, out ToeplitzMatrix m, out int[] v)
        {
            var lines = File.ReadAllLines(filepath);
            if (lines.Length < 3)
                throw new InvalidDataException();
            int n = int.Parse(lines[0]);

            var a = lines[1].Split(' ');
            var a2 = a.Select(x => int.Parse(x)).ToArray();
            if (a.Length != 2 * n - 1)
                throw new InvalidDataException();
            a.Reverse();
            
            int[] vect = lines[2].Split(' ').Select(x => int.Parse(x)).ToArray();
            if (vect.Length != n)
                throw new InvalidDataException();

            m = new ToeplitzMatrix(a2);
            v = vect;
        }
        public static void WriteData(string filepath, int[] v)
        {
            string[] lines = new string[2];
            lines[0] = v.Length.ToString();
            lines[1] = "";

            foreach(int elem in v)
            {
                lines[1] += elem + " ";
            }

            File.WriteAllLines(filepath, lines);
        }

        public static string GetResultFilepath(string filepath)
        {
            string directory = Path.Combine(Path.GetDirectoryName(filepath), "Results");
            return Path.Combine(directory, Path.GetFileNameWithoutExtension(filepath) + "_result.txt");
        }
    }
}
