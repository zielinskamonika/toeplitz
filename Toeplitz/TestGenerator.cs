using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toeplitz
{
    class TestGenerator
    {
        public static void GenerateTests(int testCount, int minSize, int sizeStep, string testDirectory = "..\\..\\TestingData")
        {
            if (!testDirectory.EndsWith("\\"))
            {
                testDirectory += "\\";
            }
            Random rand = new Random();

            for(int i = 0; i < testCount; i++)
            {
                int n = minSize + i * sizeStep;

                string filename = "test_" + (n) + ".txt";
                string[] lines = new string[3];
                
                lines[0] = n.ToString();

                for (int j = 0; j < 2 * n - 1; j++)
                {
          //40 000
                    lines[1] += Math.Round(rand.NextDouble() * 40000, 4);
                    if (j != 2 * n - 2)
                        lines[1] += " ";
                }

                for (int j = 0; j < n; j++)
                {
                    lines[2] += Math.Round(rand.NextDouble() * 100, 4);
                    if (j != n - 1)
                        lines[2] += " ";
                }

                File.WriteAllLines(testDirectory + filename, lines);
            }
        }
    }
}
