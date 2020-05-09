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
            Console.WriteLine("Podaj lokalizacje folderu z testami lub pliku testowego wzgledem biezacego katalogu.");
            string path = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Nie została podana ścieżka.");
                return;
            }

            FileAttributes attr = File.GetAttributes(path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                TestRunner.RunAllTests(path, true);
                Console.WriteLine("Wyniki mnożenia zostaly zapisane w podanej lokalizacji w folderze Results");
            }
            else
            {
                TestRunner.RunTest(path, true, true);
                Console.WriteLine("Wynik zostal zapisany w podanej lokalizacji w folderze Results.");
            }

            Console.ReadLine();
        }
    }
}
