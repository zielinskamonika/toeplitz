using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toeplitz
{
    class Program
    {
        static void Main(string[] args)
        {
            //Generuje testy
            //TestGenerator.GenerateTests(100, 1, 1);

            //Sprawdza wszystkie testy
            //TestRunner.RunAllTests("..\\..\\TestingData");

            //Sprawdza konkretny test
            TestRunner.RunTest("..\\..\\TestingData\\test_3.txt", true);

            /* Odkomentować na koniec
             * 
            Console.WriteLine("Podaj lokalizacje folderu z testami lub pliku testowego wzgledem biezacego katalogu. " +
                "Domyslnie zostanie wybrany folder TestingData.");
            string path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
                path = "..\\..\\TestingData";

            FileAttributes attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Console.WriteLine("Wyniki zostaną zapisane w podanym miejscu w folderze Results");
                TestRunner.RunAllTests(path, true);
            }
            else
            {
                TestRunner.RunTest(path, true);
            }*/
        }
    }
}
