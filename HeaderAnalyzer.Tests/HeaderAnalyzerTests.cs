using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using Tools.Interfaces;
using Xunit;

namespace HeaderAnalyzer.Tests
{
    public class HeaderAnalyzerTests
    {

        [Fact]
        public async void SetKeywords_UrlIncorrect()
        {
            var url = "www.wp.pl"; //Url is incorrect 'caused by passing it without protocol

            var logger = Substitute.For<ILogger<Tools.HeaderAnalyzer>>();

            var headerAnalyzer = new Tools.HeaderAnalyzer(logger);

            await headerAnalyzer.SetKeywords(url);

            Assert.Null(headerAnalyzer.Keywords);
        }

        [Fact]
        public async void SetKeywords_UrlCorrect()
        {
            var url = "http://www.wp.pl";

            var logger = Substitute.For<ILogger<Tools.HeaderAnalyzer>>();

            var headerAnalyzer = new Tools.HeaderAnalyzer(logger);

            await headerAnalyzer.SetKeywords(url);

            Assert.NotNull(headerAnalyzer.Keywords);
        }

        [Fact]
        public async void CountKeywordsOccurences_UrlCorrect()
        {
            var url = "http://www.wp.pl";

            var logger = Substitute.For<ILogger<Tools.HeaderAnalyzer>>();

            var headerAnalyzer = new Tools.HeaderAnalyzer(logger);

            await headerAnalyzer.SetKeywords(url);
            var result = await headerAnalyzer.CountKeywordsOccurences(url);

            Assert.NotNull(result);
        }

        [Fact]
        public async void CountKeywordsOccurences_UrlIncorrect()
        {
            var url = "www.wp.pl";

            var logger = Substitute.For<ILogger<Tools.HeaderAnalyzer>>();

            var headerAnalyzer = new Tools.HeaderAnalyzer(logger);

            await headerAnalyzer.SetKeywords(url);
            var result = await headerAnalyzer.CountKeywordsOccurences(url);

            Assert.Null(result);
        }
    }
}
