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
                string filename = "test_" + (i+1) + ".txt";
                string[] lines = new string[3];
                
                int n = minSize + i * sizeStep;
                lines[0] = n.ToString();

                for (int j = 0; j < 2 * n - 1; j++)
                {
                    lines[1] += rand.Next(0, 100);
                    if (j != 2 * n - 2)
                        lines[1] += " ";
                }

                for (int j = 0; j < n; j++)
                {
                    lines[2] += rand.Next(0, 100);
                    if (j != n - 1)
                        lines[2] += " ";
                }

                File.WriteAllLines(testDirectory + filename, lines);
            }
        }
    }
}
