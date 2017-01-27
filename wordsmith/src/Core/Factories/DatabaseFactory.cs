using NPoco;
using System.Data.SqlClient;
using WordSmith.Core.Factories.Interfaces;
using WordSmith.Core.Wrappers.Interfaces;

namespace WordSmith.Core.Factories
{
        public class DatabaseFactory : IDatabaseFactory {
        private readonly IConfigWrapper _configWrapper;

        public DatabaseFactory(IConfigWrapper configWrapper) {
            this._configWrapper = configWrapper;
        }
        public IDatabase GetDatabase() {
            //todo factory 
            return new Database(this._configWrapper.DbConnectionString, this._configWrapper.DbType, SqlClientFactory.Instance);
        }
    }
}
