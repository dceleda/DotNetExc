using System;
using System.Buffers;
using System.IO;

namespace ArrayPoolExc
{
    public class ArrayPoolExc01
    {
        private string _path = "photo.jpg";

        private FileStream GetStream() => new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read);

        public int ReadStreamWithOneArray(int bufferSize)
        {          
            var buffer = new byte[bufferSize];

            var reads = 0;

            using (var reader = GetStream())
            {
                while(reader.Read(buffer, 0, buffer.Length) > 0)
                {
                    // ...
                    reads++;
                }
            }

            //Console.WriteLine(reads);
            return reads;
        }

        

        public int ReadStreamWithManyArrays(int bufferSize)
        {
            var reads = 0;

            using (var reader = GetStream())
            {
                var buffer = new byte[bufferSize];
                while (reader.Read(buffer, 0, buffer.Length) > 0)
                {
                    buffer = new byte[bufferSize];
                    reads++;
                }
            }

            return reads;
        }

        public int ReadStreamWithOneRent(int bufferSize)
        {
            var reads = 0;

            var pool = ArrayPool<byte>.Shared;

            using (var reader = GetStream())
            {
                var buffer = pool.Rent(bufferSize);
                while (reader.Read(buffer, 0, buffer.Length) > 0)
                {
                    // ...
                    reads++;
                }

                pool.Return(buffer);
            }

            return reads;
        }

        public int ReadStreamWithManyRents(int bufferSize)
        {
            var reads = 0;

            var pool = ArrayPool<byte>.Shared;

            using (var reader = GetStream())
            {
                var buffer = pool.Rent(bufferSize);
                var read = reader.Read(buffer, 0, buffer.Length);
                while (read > 0)
                {
                    // get data from buffer
                    // ...
                    pool.Return(buffer);
                    reads++;

                    buffer = pool.Rent(bufferSize);
                    read = reader.Read(buffer, 0, buffer.Length);
                }
            }

            return reads;
        }
    }
}
