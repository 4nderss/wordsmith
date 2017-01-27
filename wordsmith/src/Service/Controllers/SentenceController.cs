using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WordSmith.Core.Managers.Interfaces;

namespace Service.Controllers {
    [Route("/api/sentence/")]
    public class SentenceController : Controller {

        private readonly ISentenceManager _sentenceManager;
        private readonly IDatabaseManager _databaseManager;

        public SentenceController(ISentenceManager sentenceManager, IDatabaseManager databaseManager) {
            this._sentenceManager = sentenceManager;
            this._databaseManager = databaseManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransformedSentence(string input) {
            var logTask = this._databaseManager.LogSentenceAsync(input);
            var sentenceTask = this._sentenceManager.TransformWordAsync(input);

            await Task.WhenAll(logTask, sentenceTask).ConfigureAwait(false);

            var result = sentenceTask.Result;

            return this.Json(result);
        }
    }
}
