using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.Interfaces;
using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tools
{
    public class HeaderAnalyzer : IHeaderAnalyzer
    {
        private readonly HtmlWeb _htmlWeb;
        private readonly ILogger<HeaderAnalyzer> _logger;

        public IEnumerable<string> Keywords { get; set; }
        public string Html { get; set; }
        public string ErrorMsg { get; set; }

        public HeaderAnalyzer(ILogger<HeaderAnalyzer> logger)
        {
            _htmlWeb = new HtmlWeb();
            _logger = logger;
        }

        public async Task SetKeywords(string url)
        {
            try
            {
                var htmlDocument = await _htmlWeb.LoadFromWebAsync(url);

                Html = htmlDocument.ParsedText;

                var keywordsNode = htmlDocument.DocumentNode.SelectSingleNode("//head/meta[@name='Keywords']");

                if (keywordsNode == null)
                {
                    keywordsNode = htmlDocument.DocumentNode.SelectSingleNode("//head/meta[@name='keywords']");
                }

                var keywordsFromPage = keywordsNode?.Attributes["content"].Value;

                Keywords = keywordsFromPage?.Split(',').ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                ErrorMsg = "Something went wrong. Probably url is not correct. Please check this and try again.";
            }
        }

        public async Task<Dictionary<string, int>> CountKeywordsOccurences(string url)
        {
            var validUrl = PrepareUrl(url);

            if (string.IsNullOrEmpty(validUrl))
            {
                ErrorMsg = "Url is not valid. Please correct it.";
                _logger.LogError($"Url is not valid. Url passed: {url}");
                return null;
            }

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            Regex rgx;

            if (!string.IsNullOrEmpty(ErrorMsg) || Keywords == null)
                return null;

            foreach (var keyword in Keywords)
            {
                var pattern = @"\b" + keyword + @"\b";
                rgx = new Regex(pattern);

                var matchesCount = rgx.Matches(Html).Count();
                
                if (dictionary.ContainsKey(keyword))
                {
                    dictionary[keyword] += matchesCount;
                }
                else
                {
                    dictionary.Add(keyword, matchesCount);
                }
                
            }

            return dictionary;
        }

        private string PrepareUrl(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if(uriResult != null)
            {
                return uriResult.AbsoluteUri;
            }

            return null;
        }
    }
}
