using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordSmith.Core.Managers.Interfaces;

namespace WordSmith.Core.Managers {
    public class SentenceManager : ISentenceManager {
      
        public async Task<string> TransformWordAsync(string input) {

            return await Task.Run<string>(() => {

                string pattern = @"(\S+\b)";
                var matches = Regex.Matches(input, pattern);

                var words = matches;

                string res = input;
                for (int i = 0; i < words.Count; i++) {
                    var word = words[i].Value;
                    var transformed = string.Empty;
                    for (int j = word.Length - 1; j >= 0; j--) {
                        var character = word[j];
                        transformed += character;
                    }

                    var escapedWord = Regex.Escape(word);
                    var regex = new Regex(string.Format(@"(\b|\B){0}(\b|\B)", escapedWord)); // /b tar word-boundry ord men /B måste även med för att hantera ord som börjar med tex . (.abc)
                    res = regex.Replace(res, transformed, 1);

                }

                return res;
            }).ConfigureAwait(false);
        }
    }
}
