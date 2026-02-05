using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic;
using Xunit;
using FluentAssertions;

namespace AnagramSolver.Tests
{
    public class WordNormalizerTests
    {
        [Fact]
        public void NormalizeUserWords_ShouldSplitBySpacesTabsAndJoin()
        {
            var words = "labas rytas\tkava";

            var normalizer = new WordNormalizer();

            var results = normalizer.NormalizeUserWords(words);

            results.Should().Be("labasrytaskava");
        }

        [Fact]
        public void NormalizeUserWords_ShouldRemoveLeadingAndTrailingSpaces()
        {
            var words = " labas rytas ";

            var normalizer = new WordNormalizer();

            var results = normalizer.NormalizeUserWords(words);

            results.Should().Be("labasrytas");
        }

        [Fact]
        public void NormalizeUserWords_ShouldTurnWordsToLowerCase()
        {
            var words = "LAbaS rYTas";

            var normalizer = new WordNormalizer();

            var results = normalizer.NormalizeUserWords(words);

            results.Should().Be("labasrytas");
        }

        [Fact]
        public void NormalizeUserWords_ShouldRemoveEmptyEntries()
        {
            var words = " labas  \t     rytas    kava";

            var normalizer = new WordNormalizer();

            var results = normalizer.NormalizeUserWords(words);

            results.Should().Be("labasrytaskava");
        }

        [Fact]
        public void NormalizeUserWords_WhenEmptyInput_ShouldReturnEmptyString()
        {
            var words = "";

            var normalizer = new WordNormalizer();

            var results = normalizer.NormalizeUserWords(words);

            results.Should().BeEmpty();
        }

        [Fact]
        public void NormalizeUserWords_WhenNoInput_ShouldReturnEmptyString()
        {
            string words = null;

            var normalizer = new WordNormalizer();

            var results = normalizer.NormalizeUserWords(words);

            results.Should().BeEmpty();
        }

        [Fact]
        public void NormalizeFileWords_ShouldRemoveLeadingAndTrailingSpaces()
        {
            var words = new List<string> { " labas " };

            var normalizer = new WordNormalizer();

            var results = normalizer.NormalizeFileWords(words);

            results.Should().Contain("labas");
        }

        [Fact]
        public void NormalizeFileWords_ShouldTurnWordsToLowerCase()
        {
            var words = new List<string> { "lABaS" };

            var normalizer = new WordNormalizer();

            var results = normalizer.NormalizeFileWords(words);

            results.Should().Contain("labas");
        }

        [Fact]
        public void NormalizeFileWords_ShouldNotContainDuplicates()
        {
            var words = new List<string> { "labas", "labas" };

            var normalizer = new WordNormalizer();

            var results = normalizer.NormalizeFileWords(words);

            results.Should().ContainSingle();
        }

        [Fact]
        public void NormalizeFileWords_WhenNoInput_ShouldReturnEmptySet()
        {
            IEnumerable<string> words = null;

            var normalizer = new WordNormalizer();

            var results = normalizer.NormalizeFileWords(words);

            results.Should().BeEmpty();
        }
    }
}
