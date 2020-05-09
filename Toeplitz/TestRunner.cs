﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toeplitz
{
    class TestRunner
    {
        public static void RunAllTests(string testDirectory, bool saveResults = false)
        {
            var files = Directory.GetFiles(testDirectory);
            Directory.CreateDirectory(testDirectory + "\\Results");
            foreach(var file in files)
            {
                RunTest(file, false, true);
                Console.WriteLine("------------------------------------------");
            }
        }
        public static void RunTest(string testPath, bool verboseMode = false, bool saveResults = false)
        {
            ToeplitzMatrix m;
            int[] v;
            int[] wClassic, wFast;
            TimeSpan classicTime, fastTime;
            
            Console.WriteLine("Test " + testPath);
            DataHandler.ReadData(testPath, out m, out v);

            if (verboseMode)
            {
                Console.WriteLine("Macierz:");
                Console.WriteLine(m);
                Console.WriteLine("Wektor (transponowany):");
                for (int i = 0; i < v.Length; i++)
                {
                    Console.Write(v[i] + " ");
                }
                Console.WriteLine();
            }

            Stopwatch watch = new Stopwatch();

            watch.Start();
            wClassic = m.ClassicMultiply(v);
            watch.Stop();
            classicTime = watch.Elapsed;

            watch.Restart();
            wFast = m.FastMultiply(v);
            watch.Stop();
            fastTime = watch.Elapsed;

            bool correct = true;

            for(int i = 0; i < wClassic.Length; i++)
            {
                if (wClassic[i] != wFast[i])
                {
                    correct = false;
                    break;
                }
            }

            if (correct)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK");
                Console.ResetColor();
                Console.WriteLine($"Rozmiar problemu: {wClassic.Length}");
                Console.WriteLine($"Czas mnożenia klasycznego: {classicTime.TotalSeconds}s");
                Console.WriteLine($"Czas mnożenia szybkiego: {fastTime.TotalSeconds}s");

                if (verboseMode)
                {
                    Console.WriteLine("Wynik:");
                    for (int i = 0; i < wClassic.Length; i++)
                    {
                        Console.Write(wClassic[i] + " ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BŁĘDNY WYNIK");
                Console.ResetColor();
                Console.WriteLine($"Rozmiar problemu: {wClassic.Length}");

                if (verboseMode)
                {
                    Console.WriteLine("Wynik:");
                    for (int i = 0; i < wFast.Length; i++)
                    {
                        Console.Write(wFast[i] + " ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Oczekiwany wynik:");
                    for (int i = 0; i < wClassic.Length; i++)
                    {
                        Console.Write(wClassic[i] + " ");
                    }
                    Console.WriteLine();
                }
            }

            if (saveResults)
            {
                DataHandler.WriteData(DataHandler.GetResultFilepath(testPath), wFast);
            }
        }
    }
}
