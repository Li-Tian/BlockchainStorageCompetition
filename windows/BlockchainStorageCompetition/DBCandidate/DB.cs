using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neo.ngd.bsc.db
{
    public interface DB
    {
        void Put(byte[] key, byte[] value);
        byte[] Get(byte[] key);
        string GetName();
        string GetAuthorName();
        string GetAuthorEmail();
        string GetDescription();
    }
}
