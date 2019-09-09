using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeaderAnalyzer.Models
{
    public class AnalyzeModel
    {
        public AnalyzeModel()
        {
        }

        /// <summary>
        /// List of keyword founded on specified page
        /// </summary>
        public IEnumerable<string> Keywords { get; set; }

        /// <summary>
        /// Result of analyze
        /// </summary>
        public Dictionary<string, int> Results { get; set; }
        

        public string KeywordsList
        {
            get => Keywords != null ? string.Join(',', Keywords) : null;
        }
    }
}
