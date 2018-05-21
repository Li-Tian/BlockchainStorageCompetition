using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neo.ngd.bsc.db
{
    public interface DB
    {
        // Initialize the database to a best situation to put or to get.
        void Init();
        // Clean up everything. Delete everything DB created. Do nothing if nothing to delete.
        void Cleanup();
        // Put key/value pair into database
        void Put(byte[] key, byte[] value);
        // Get value by key, return null if not found
        byte[] Get(byte[] key);
        // Get name of db
        string GetName();
        // Get name of author
        string GetAuthorName();
        // Get Email of author
        string GetAuthorEmail();
        // Get description of db
        string GetDescription();
    }
}
