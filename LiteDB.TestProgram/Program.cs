
using System;
using System.Threading;
using System.Collections.Generic;

namespace LiteDB.TestProgram
{
    class Program
    {
        class TestItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public DateTime Time { get; set; }
            public double Value { get; set; }

            public TestItem()
            {
                Id = Guid.NewGuid().ToString("N");
            }
        }

        static LiteDatabase DB;

        static void Main(string[] args)
        {
            DB = new LiteDatabase("username_2.db");

            var integrity = DB.IntegrityCheck();

            return;

            var thread = new Thread(WriteData);
            thread.Start();

            Thread.Sleep(200);

            thread.Abort();

            DB.Dispose();

            /////////

            Thread.Sleep(1000);

            /////////


            DB = new LiteDatabase("test.db");

            var collection = DB.GetCollection<TestItem>("test_collection");

            var item = collection.FindOne(Query.EQ("Name", 500));

            DB.Dispose();
        }

        static void WriteData()
        {
            var items = new List<TestItem> { };
            for (int idx = 0; idx < 1000; ++idx)
                items.Add(
                    new TestItem
                    {
                        Name = idx.ToString()
                    });

            var collection = DB.GetCollection<TestItem>("test_collection");
            collection.Upsert(items);
        }
    }
}

