using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeaderAnalyzer.Models
{
    public class HomeModel
    {
        public HomeModel()
        {
        }

        public string Url { get; set; }

        public AnalyzeModel AnalyzeResult { get; set; }

        public string Error { get; set; }
    }
}
