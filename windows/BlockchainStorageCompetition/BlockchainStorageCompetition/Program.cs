using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using neo.ngd.bsc.db;

// neo.ngd.bsc (Blockchain Storage Competition)
namespace neo.ngd.bsc
{
    class Program
    {
        //const int TEST_DATA_AMOUNT = 10000000; // you can use less data during developing
        const int TEST_DATA_AMOUNT = 1000000;
        //const int TEST_DATA_AMOUNT = 100000;
        const double SAMPLE_PROBABLY = 0.001;
        const int SAMPLE_POOL_SIZE = 1000;

        const int GET_PER_PUT = 10;

        const int KEY_SIZE_LOW_BOUND = 20;
        const int KEY_SIZE_HIGH_BOUND = 32;
        const int VALUE_SIZE_LOW_BOUND = 512;
        const int VALUE_SIZE_HIGH_BOUND = 20480;

        static void Main(string[] args)
        {
            Console.WriteLine("Blockchain storage competition 2018 by NEO...");

            DB db = DBFactory.GetInstance();
            Console.WriteLine(String.Format("Write {0:N0} records into {1}.", TEST_DATA_AMOUNT, db.GetName()));
            Random rnd = new Random();

            int tested_count = 0;
            int passed_count = 0;
            Dictionary<byte[], byte[]> sample_pool = new Dictionary<byte[], byte[]>();
            List<byte[]> sample_pool_keys = new List<byte[]>();

            long start = DateTime.Now.Ticks;

            for (int i = 0; i < TEST_DATA_AMOUNT; i++)
            {
                // create test data
                byte[] key = GenerateRandomKey(rnd);
                byte[] value = GenerateRandomValue(rnd);
                // write into database
                db.Put(key, value);

                // write into testing sample pool
                if (rnd.NextDouble() < SAMPLE_PROBABLY)
                {
                    Console.Write(".");
                    sample_pool.Add(key, value);
                    sample_pool_keys.Add(key);

                    int theOrderOfRealGet = rnd.Next(GET_PER_PUT);
                    for (int j = 0; j < theOrderOfRealGet; j++)
                    {
                        // Do dummy Get
                        db.Get(GenerateRandomKey(rnd));
                    }

                    byte[] sample_key = sample_pool_keys[rnd.Next(sample_pool_keys.Count)];
                    byte[] sample_value = sample_pool[sample_key];
                    tested_count++;
                    byte[] saved_value = db.Get(sample_key);
                    if (sample_value.Equals(saved_value))
                    {
                        passed_count++;
                    }
                    // if sample pool is full, remove the sample that is tested
                    if (sample_pool.Count > SAMPLE_POOL_SIZE)
                    {
                        sample_pool.Remove(sample_key);
                        sample_pool_keys.Remove(sample_key);
                    }

                    for (int j = theOrderOfRealGet + 1; j < GET_PER_PUT; j++)
                    {
                        // Do dummy Get
                        db.Get(GenerateRandomKey(rnd));
                    }
                }
                else
                {
                    for (int j = 0; j < GET_PER_PUT; j++)
                    {
                        // Do dummy Get
                        db.Get(GenerateRandomKey(rnd));
                    }
                }
            }
            long stop = DateTime.Now.Ticks;
            long cost = stop - start;
            long costPerRound = cost / TEST_DATA_AMOUNT;
            long seconds = cost / 10000000;
            long fraction = cost % 10000000;

            Console.WriteLine();
            Console.WriteLine(String.Format("DB Name         : {0}", db.GetName()));
            Console.WriteLine(String.Format("Description     : {0}", db.GetDescription()));
            Console.WriteLine(String.Format("Author          : {0}", db.GetAuthorName()));
            Console.WriteLine(String.Format("Email           : {0}", db.GetAuthorEmail()));
            Console.WriteLine(String.Format("Passed / Tested : {0} / {1}", passed_count, tested_count));
            Console.WriteLine(String.Format("Result          : {0}", (passed_count == tested_count ? "Succeeded" : "Failed")));
            Console.WriteLine(String.Format("Time            : {0}.{1:D7}", seconds, fraction));
            Console.WriteLine(String.Format("Cost Per Round  : {0}", costPerRound));
        }

        private static byte[] GenerateRandomKey(Random rnd)
        {
            int size = rnd.Next(KEY_SIZE_LOW_BOUND, KEY_SIZE_HIGH_BOUND);
            byte[] key = new byte[size];
            rnd.NextBytes(key);
            return key;
        }

        private static byte[] GenerateRandomValue(Random rnd)
        {
            int size = rnd.Next(VALUE_SIZE_LOW_BOUND, VALUE_SIZE_HIGH_BOUND);
            byte[] value = new byte[size];
            rnd.NextBytes(value);
            return value;
        }
    }
}
