using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordSmith.Core.Models.Database;

namespace WordSmith.Core.Managers.Interfaces {
    public interface IDatabaseManager {
        Task LogSentenceAsync(string sentence);

        Task<string[]> GetLastThreeSentencesAsync();

        Task<string[]> GetMostPopularSentencesAsync();

        Task<int> GetNumberOfTransformedSentencesAsync();
        Task LogErrorAsync(ErrorLog logEntry);
    }
}
