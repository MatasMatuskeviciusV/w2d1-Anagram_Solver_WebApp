using AnagramSolver.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace AnagramSolver.Tests
{
    public class AnagramMapBuilderTests
    {
        [Fact]
        public void Build_ShouldGroupAnagramsUnderTheSameKey()
        {
            var words = new List<string> { "labas", "balas" };

            var builder = new AnagramMapBuilder();
            var map = builder.Build(words);

            map.Should().ContainKey("aabls");
            map["aabls"].Should().Contain("labas", "balas");
        }

        [Fact]
        public void Build_ShouldCreateNewKeysForNewWords()
        {
            var words = new List<string> { "labas", "rytas" };

            var builder = new AnagramMapBuilder();
            var map = builder.Build(words);

            map.Should().ContainKeys("aabls", "arsty");
        }

        [Fact]
        public void Build_WhenNoInput_ShouldReturnEmptyList()
        {
            var words = new List<string>();

            var builder = new AnagramMapBuilder();
            var map = builder.Build(words);

            map.Should().BeEmpty();
        }
    }
}
