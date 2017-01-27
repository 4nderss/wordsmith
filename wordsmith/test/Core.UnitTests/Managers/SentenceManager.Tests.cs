using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WordSmith.Core.Managers;

namespace Pascal.UnitTests.Managers {
    [TestClass]
    public class SentenceManagerTests {
        private SentenceManager _sentenceManager;

        [TestInitialize]
        public void TestInitialize() {
            this._sentenceManager = new SentenceManager();               
        }

        [TestMethod]
        public async Task Test_TransformSentence_Basic() {
            var input = "Hello World";
            var expected = "olleH dlroW";

            var actual = await this._sentenceManager.TransformWordAsync(input).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual.Length, input.Length);

        }

        [TestMethod]
        public async Task Test_TransformSentence_WithCommaAndPeriod() {
            var input = "Hello World, today is a nice day.";
            var expected = "olleH dlroW, yadot si a ecin yad.";

            var actual = await this._sentenceManager.TransformWordAsync(input).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual.Length, input.Length);

        }

        [TestMethod]
        public async Task Test_TransformSentence_WithCommaAndPeriod2() {
            var input = "Hello World. Today the weather is nice.";
            var expected = "olleH dlroW. yadoT eht rehtaew si ecin.";

            var actual = await this._sentenceManager.TransformWordAsync(input).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual.Length, input.Length);
        }

        [TestMethod]
        public async Task Test_TransformSentence_WithExclamationAndQuestionMarks() {
            var input = "Hello World! Is there anybody alive out there?";
            var expected = "olleH dlroW! sI ereht ydobyna evila tuo ereht?";

            var actual = await this._sentenceManager.TransformWordAsync(input).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual.Length, input.Length);

        }

        [TestMethod]
        public async Task Test_TransformSentence_WithDigits() {
            var input = "Hello World! What is 1+5?";
            var expected = "olleH dlroW! tahW si 5+1?";
           
            var actual = await this._sentenceManager.TransformWordAsync(input).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual.Length, input.Length);

        }

        [TestMethod]
        public async Task Test_TransformSentence_WithDigitsAndSpecialCharacters() {
            var input = "Hello World! Is the time 12:34?";
            var expected = "olleH dlroW! sI eht emit 43:21?";

            var actual = await this._sentenceManager.TransformWordAsync(input).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual.Length, input.Length);

        }

        [TestMethod]
        public async Task Test_TransformSentence_WithDigitSeparatorsAndTwoTrailingSeparators() {
            var input = "Hello World! Does it cost 1,200$?";
            var expected = "olleH dlroW! seoD ti tsoc 002,1$?";

            var actual = await this._sentenceManager.TransformWordAsync(input).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual.Length, input.Length);

        }

        [TestMethod]
        public async Task Test_TransformSentence_MultipleEndingSeparators() {
            var input = "Hello World!?!?!";
            var expected = "olleH dlroW!?!?!"; 

            var actual = await this._sentenceManager.TransformWordAsync(input).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual.Length, input.Length);

        }

        [TestMethod]
        public async Task Test_TransformSentence_MultipleWhiteSplaces() {
            var input = "Hello       World!?!?!";
            var expected = "olleH       dlroW!?!?!";

            var actual = await this._sentenceManager.TransformWordAsync(input).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual.Length, input.Length);

        }

        [TestMethod]
        public async Task Test_TransformSentence_WordsThatStartsWithWordBoundry() {
            var input = "Hello .World!";
            var expected = "olleH dlroW.!";

            var actual = await this._sentenceManager.TransformWordAsync(input).ConfigureAwait(false);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(actual.Length, input.Length);

        }




    }
}
