using ArrayPoolExc;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkApp
{
    [SimpleJob(RuntimeMoniker.CoreRt50)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [MemoryDiagnoser]
    public class ArrayPoolBenchmark
    {
        [Params(10, 100, 1000, 4096, 10000, 100000)]
        public int BufferSize;

        [Params(1, 10)]
        public int Connections;

        [Benchmark]
        public async Task ReadStreamWithOneArray()
        {
            await RunMany(() => new ArrayPoolExc01().ReadStreamWithOneArray(BufferSize));
        }

        [Benchmark]
        public async Task ReadStreamWithManyArrays()
        {
            await RunMany(() => new ArrayPoolExc01().ReadStreamWithManyArrays(BufferSize));
        }

        [Benchmark]
        public async Task ReadStreamWithOneRent()
        {
            await RunMany(() => new ArrayPoolExc01().ReadStreamWithOneRent(BufferSize));
        }

        [Benchmark]
        public async Task ReadStreamWithManyRents()
        {
            await RunMany(() => new ArrayPoolExc01().ReadStreamWithManyRents(BufferSize));
        }

        [Benchmark]
        public async Task ReadStreamWithManyRentsNoReturns()
        {
            await RunMany(() => new ArrayPoolExc01().ReadStreamWithManyRents(BufferSize, false));
        }

        private async Task RunMany(Action action)
        {
            await Task.WhenAll(Enumerable.Range(0, Connections).Select((i) => { return Task.Factory.StartNew(action); }));
        }
    }
}
