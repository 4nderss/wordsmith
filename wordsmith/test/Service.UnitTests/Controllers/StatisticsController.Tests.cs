using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Service.Controllers;
using Service.Controllers.ResultModels;
using System.Threading.Tasks;
using WordSmith.Core.Managers.Interfaces;

namespace Service.UnitTests.Controllers {
    [TestClass]
    public class StatisticsControllerTests {
        private StatisticsController _statisticsController;

        private Mock<IDatabaseManager> _mockedDatabaseManager;

        [TestInitialize]
        public void TestInitialize() {
            this._mockedDatabaseManager = new Mock<IDatabaseManager>();


            this._statisticsController = new StatisticsController(_mockedDatabaseManager.Object);
        }


        [TestMethod]
        public async Task Test_StatisticsControllerTests_DoesReturnCorrectStructure() {


            var actualResponse = await this._statisticsController.GetStatistics().ConfigureAwait(false);
            Assert.IsTrue(actualResponse is JsonResult);

            var actualJsonValue = (actualResponse as JsonResult).Value;

            Assert.IsTrue(actualJsonValue is StatisticsResultModel);

        }

        [TestMethod]
        public async Task Test_StatisticsControllerTests_ReturnsCorrectLastThreeSentences() {

            var expected = new[] { "Sentence 1", "Sentence 2", "Sentence 3" };


            this._mockedDatabaseManager
                .Setup(db => db.GetLastThreeSentencesAsync())
                .Returns(() => {
                    return Task.FromResult(expected);
                });


            var actualResponse = await this._statisticsController.GetStatistics().ConfigureAwait(false);
            Assert.IsTrue(actualResponse is JsonResult);

            StatisticsResultModel actualJsonValue = (StatisticsResultModel)(actualResponse as JsonResult).Value;

            Assert.AreEqual(expected, actualJsonValue.MostRecentSentences);

        }


        [TestMethod]
        public async Task Test_StatisticsControllerTests_ReturnsCorrectPopularSentences() {

            var expected = new[] { "Popular 1", "Popular 2", "Popular 3", "Popular 4", "Popular 5" };


            this._mockedDatabaseManager
                .Setup(db => db.GetMostPopularSentencesAsync())
                .Returns(() => {
                    return Task.FromResult(expected);
                });


            var actualResponse = await this._statisticsController.GetStatistics().ConfigureAwait(false);
            Assert.IsTrue(actualResponse is JsonResult);

            StatisticsResultModel actualJsonValue = (StatisticsResultModel)(actualResponse as JsonResult).Value;

            Assert.AreEqual(expected, actualJsonValue.MostPopularSentences);

        }


        [TestMethod]
        public async Task Test_StatisticsControllerTests_ReturnsCorrectTotalSentences() {

            var expected = 15;


            this._mockedDatabaseManager
                .Setup(db => db.GetNumberOfTransformedSentencesAsync())
                .Returns(() => {
                    return Task.FromResult(expected);
                });


            var actualResponse = await this._statisticsController.GetStatistics().ConfigureAwait(false);
            Assert.IsTrue(actualResponse is JsonResult);

            StatisticsResultModel actualJsonValue = (StatisticsResultModel)(actualResponse as JsonResult).Value;

            Assert.AreEqual(expected, actualJsonValue.TotalSentences);

        }

        [TestMethod]
        public async Task Test_StatisticsControllerTests_ReturnsCorrectResult() {

            var expectedCount = 15;
            var expectedPopular = new[] { "Popular 1", "Popular 2", "Popular 3", "Popular 4", "Popular 5" };
            var expectedRecent = new[] { "Sentence 1", "Sentence 2", "Sentence 3" };


            this._mockedDatabaseManager
                .Setup(db => db.GetNumberOfTransformedSentencesAsync())
                .Returns(() => {
                    return Task.FromResult(expectedCount);
                });
            this._mockedDatabaseManager
               .Setup(db => db.GetMostPopularSentencesAsync())
               .Returns(() => {
                   return Task.FromResult(expectedPopular);
               });
            this._mockedDatabaseManager
             .Setup(db => db.GetLastThreeSentencesAsync())
             .Returns(() => {
                 return Task.FromResult(expectedRecent);
             });


            var actualResponse = await this._statisticsController.GetStatistics().ConfigureAwait(false);
            Assert.IsTrue(actualResponse is JsonResult);

            StatisticsResultModel actualJsonValue = (StatisticsResultModel)(actualResponse as JsonResult).Value;

            Assert.AreEqual(expectedPopular, actualJsonValue.MostPopularSentences);
            Assert.AreEqual(expectedRecent, actualJsonValue.MostRecentSentences);
            Assert.AreEqual(expectedCount, actualJsonValue.TotalSentences);

        }
    }
}