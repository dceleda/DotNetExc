using ArrayPoolExc;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

namespace BenchmarkApp
{
    class Program
    {


        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ArrayPoolBenchmark>();

            Console.ReadKey();
        }
    }
}
