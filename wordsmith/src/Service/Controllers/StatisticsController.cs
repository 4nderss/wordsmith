using Microsoft.AspNetCore.Mvc;
using Service.Controllers.ResultModels;
using System.Threading.Tasks;
using WordSmith.Core.Managers.Interfaces;

namespace Service.Controllers {
    [Route("/api/statistics/")]
    public class StatisticsController : Controller {

        private readonly ISentenceManager _sentenceManager;
        private readonly IDatabaseManager _databaseManager;

        public StatisticsController(IDatabaseManager databaseManager) {
            this._databaseManager = databaseManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistics() {
            var recentSentencesTask = this._databaseManager.GetLastThreeSentencesAsync();
            var totalSentencesTask = this._databaseManager.GetNumberOfTransformedSentencesAsync();
            var popularSentencesTask = this._databaseManager.GetMostPopularSentencesAsync();

            await Task.WhenAll(recentSentencesTask, totalSentencesTask, popularSentencesTask).ConfigureAwait(false);

            var mostRecentSentences = recentSentencesTask.Result;
            var totalSentences = totalSentencesTask.Result;
            var popularSentences = popularSentencesTask.Result;

            var resultModel = new StatisticsResultModel();
            resultModel.MostRecentSentences = mostRecentSentences;
            resultModel.TotalSentences = totalSentences;
            resultModel.MostPopularSentences = popularSentences;

            return this.Json(resultModel);
        }
    }
}
