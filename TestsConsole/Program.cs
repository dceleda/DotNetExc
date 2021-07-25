using ArrayPoolExc;
using BenchmarkApp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TestsConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int connections = 10;
            int bufferSize = 100;

            //var exc = new ArrayPoolExc01();
            //await Task.WhenAll(Enumerable.Range(0, connections).Select((i) => { return Task.Factory.StartNew(() => { new ArrayPoolExc01().ReadStreamWithOneArray(bufferSize); }); }));

            var bench = new ArrayPoolBenchmark() { BufferSize = 100, Connections = 10 };
            await bench.ReadStreamWithOneArray();


            //Console.WriteLine($"Reads:{reads}");
        }
    }
}
