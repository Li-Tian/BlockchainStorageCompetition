using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neo.ngd.bsc.db
{
    public class DBFactory
    {
        private static DB inst;

        public static DB GetInstance()
        {
            // Don't care about the concurrency issue. Only single thread in the test
            if (inst == null)
            {
                inst = new DummyDB();
                //inst = new MemoryDB();
            }
            return inst;
        }

    }
}
