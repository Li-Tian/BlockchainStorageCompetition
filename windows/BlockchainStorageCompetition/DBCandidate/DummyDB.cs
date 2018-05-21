using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neo.ngd.bsc.db
{
    internal class DummyDB : DB
    {
        public void Init()
        {
            // Nothing to initialize.
        }

        public void Cleanup()
        {
            // Nothing to clean.
        }
        public byte[] Get(byte[] key)
        {
            // get the value of the key. return null if not found
            return null;
        }

        public void Put(byte[] key, byte[] value)
        {
            // put key / value into database
        }

        public string GetName()
        {
            return "DummyDB";
        }
        public string GetAuthorName()
        {
            return "Player Unknown";
        }
        public string GetAuthorEmail()
        {
            return "YourName@YourDomain.com";
        }
        public string GetDescription()
        {
            return "DummyDB just do nothing.";
        }

    }
}
