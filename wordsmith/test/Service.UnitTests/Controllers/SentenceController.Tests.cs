using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Service.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordSmith.Core.Managers.Interfaces;

namespace Service.UnitTests.Controllers {
    [TestClass]
    public class SentenceControllerTests {
        private SentenceController _sentenceController;

        private Mock<ISentenceManager> _mockedSentenceManager;
        private Mock<IDatabaseManager> _mockedDatabaseManager;

        [TestInitialize]
        public void TestInitialize() {
           this._mockedSentenceManager = new Mock<ISentenceManager>();
           this._mockedDatabaseManager = new Mock<IDatabaseManager>();


            this._sentenceController = new SentenceController(_mockedSentenceManager.Object, _mockedDatabaseManager.Object);
        }

        [TestMethod]
        public async Task Test_TransformController_InputMatches() {

            var mySentence = "Hello World";


            this._mockedDatabaseManager
                .Setup(db => db.LogSentenceAsync(It.IsAny<string>()))
                .Returns((string sentence) => {
                    Assert.AreEqual(mySentence, sentence);
                    return Task.CompletedTask;

                });

            this._mockedSentenceManager
              .Setup(db => db.TransformWordAsync(It.IsAny<string>()))
              .Returns((string input) => {
                  Assert.AreEqual(mySentence, input);
                  return Task.FromResult(string.Empty);
              });


            await this._sentenceController.GetTransformedSentence(mySentence);



        }
        [TestMethod]
        public async Task Test_TransformController_DoesLog() {

            var mySentence = "Hello World";


            this._mockedDatabaseManager
                .Setup(db => db.LogSentenceAsync(It.IsAny<string>()))
                .Returns((string sentence) => {
                    return Task.CompletedTask;

                })
                .Verifiable();

            this._mockedSentenceManager
              .Setup(db => db.TransformWordAsync(It.IsAny<string>()))
              .Returns((string input) => {
                  return Task.FromResult(string.Empty);
              });


            await this._sentenceController.GetTransformedSentence(mySentence);

            this._mockedDatabaseManager.Verify();


        }

        [TestMethod]
        public async Task Test_TransformController_DoesTransform() {

            var mySentence = "Hello World";
         

            this._mockedSentenceManager
              .Setup(db => db.TransformWordAsync(It.IsAny<string>()))
              .Returns((string input) => {
                  return Task.FromResult(string.Empty);
              })
              .Verifiable();


            await this._sentenceController.GetTransformedSentence(mySentence);

            this._mockedSentenceManager.Verify();

        }


        [TestMethod]
        public async Task Test_TransformController_DoesReturnCorrectValues() {

            var mySentence = "Hello World";
            var expectedSentence = "olleH dlroW";

            this._mockedSentenceManager
              .Setup(db => db.TransformWordAsync(It.IsAny<string>()))
              .Returns((string input) => {
                  return Task.FromResult(expectedSentence);
              });


            var actualResponse = await this._sentenceController.GetTransformedSentence(mySentence).ConfigureAwait(false);
            Assert.IsTrue(actualResponse is JsonResult);

            var actualJsonValue = (actualResponse as JsonResult).Value;
            Assert.AreEqual(expectedSentence, actualJsonValue);


        }
    }
}