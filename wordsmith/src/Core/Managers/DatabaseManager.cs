using NPoco;
using System;
using System.Linq;
using System.Threading.Tasks;
using WordSmith.Core.Factories.Interfaces;
using WordSmith.Core.Managers.Interfaces;
using WordSmith.Core.Models.Database;

namespace WordSmith.Core.Managers {
    public class DatabaseManager : IDatabaseManager {

        private IDatabaseFactory _databaseFactory;
        private IDatabase _database;
        private IDatabase Database {
            get {
                if (this._database == null) {
                    return this._databaseFactory.GetDatabase();
                }

                return this._database;
            }
            set {
                this._database = value;
            }
        }

        public DatabaseManager(IDatabaseFactory databaseFactory) {
            this._databaseFactory = databaseFactory;
        }
        public async Task LogSentenceAsync(string sentence) {
            var dbObject = new SentenceLog();
            dbObject.LogDate = DateTime.Now;
            dbObject.Sentence = sentence;

            await this.Database.InsertAsync(dbObject).ConfigureAwait(false);
        }

        public async Task<string[]> GetLastThreeSentencesAsync() {
            var logs = await this.Database.FetchAsync<SentenceLog>("SELECT TOP 3 * FROM tSentenceLog ORDER BY ID DESC").ConfigureAwait(false);
            if (logs != null) {
                return logs.Select(x => x.Sentence).ToArray();
            }
            return null;
        }

        public async Task<string[]> GetMostPopularSentencesAsync() {

          

            var sql = @"
                        SELECT TOP 5 
                        COUNT(*) as Hits,
                        t.Sentence

                        FROM tSentenceLog t

                        GROUP by t.Sentence

                        ORDER by Hits DESC";

            var logs = await this.Database.FetchAsync<SentenceLog>(sql).ConfigureAwait(false);
            if (logs != null) {
                return logs.Select(x => x.Sentence).ToArray();
            }
            return null;
        }

        public async Task<int> GetNumberOfTransformedSentencesAsync() {
            var sql = @"SELECT COUNT(ID) FROM tSentenceLog";
            var count = await this.Database.QueryAsync<int>(sql).ConfigureAwait(false);
            if (count != null) {
                return count.FirstOrDefault();
            }
            return 0;
        }

        public async Task LogErrorAsync(ErrorLog logEntry) {
            await this.Database.InsertAsync(logEntry).ConfigureAwait(false);
        }
    }
}



