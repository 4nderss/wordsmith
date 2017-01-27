
using System.Threading.Tasks;

namespace WordSmith.Core.Managers.Interfaces {
    public interface ISentenceManager {
        /// <summary>
        /// Transforms a sentence by swapping order of the letters in each word
        /// </summary>
        /// <param name="input">Sentence to transform</param>
        /// <returns></returns>
        Task<string> TransformWordAsync(string input);
    }
}
