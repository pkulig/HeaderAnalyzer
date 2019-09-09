using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Interfaces
{
    public interface IHeaderAnalyzer
    {
        IEnumerable<string> Keywords { get; set; }
        string Html { get; set; }
        string ErrorMsg { get; set; }

        Task<Dictionary<string, int>> CountKeywordsOccurences(string url);
        Task SetKeywords(string url);
    }
}
