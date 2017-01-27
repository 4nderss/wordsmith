using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPoco;
using WordSmith.Core.Wrappers.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Service.Infrastructure {
    public class ConfigWrapper : IConfigWrapper {
        private readonly IConfigurationRoot _config;
        public ConfigWrapper(IConfigurationRoot configRoot) {
            this._config = configRoot;
        }
        public string DbConnectionString {
            get {
                return this._config["Database:ConnectionString"];
            }
        }

        public DatabaseType DbType {
            get {

                var typeName = this._config["Database:Type"];

                var type = DatabaseType.Resolve(typeName, null);
                if(type != null) {
                    return type;
                }
                return null;
            }
        }
    }
}
