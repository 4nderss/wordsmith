using NPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordSmith.Core.Wrappers.Interfaces
{
    public interface IConfigWrapper {
        string DbConnectionString { get;  }
        DatabaseType DbType { get; }

    }
}
