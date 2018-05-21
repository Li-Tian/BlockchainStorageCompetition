using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neo.ngd.bsc.db
{
    // It can be used to evaluate db performance.
    // If put too many data, it would cause OutOfMemoryException
    internal class MemoryDB : DB
    {
        private Dictionary<byte[], byte[]> dictionary = new Dictionary<byte[], byte[]>();

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
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return null;
            }
        }

        public void Put(byte[] key, byte[] value)
        {
            dictionary.Add(key, value);
        }

        public string GetName()
        {
            return "MemoryDB";
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
            return "MemoryDB just save data in memory but...OutOfMemoryException";
        }
    }
}
