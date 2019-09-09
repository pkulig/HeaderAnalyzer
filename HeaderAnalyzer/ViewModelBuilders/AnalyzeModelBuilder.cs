using HeaderAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools.Interfaces;

namespace HeaderAnalyzer.ViewModelBuilders
{
    public class AnalyzeModelBuilder : BaseModelBuilder<AnalyzeModel>
    {
        private readonly string _url;
        private readonly IHeaderAnalyzer _headerAnalyzer;

        public string Error { get; set; }

        public AnalyzeModelBuilder(string url, IHeaderAnalyzer headerAnalyzer)
        {
            _url = url;
            _headerAnalyzer = headerAnalyzer;
        }

        public override async Task<AnalyzeModel> Build()
        {
            await _headerAnalyzer.SetKeywords(_url);
            var occurences = await _headerAnalyzer.CountKeywordsOccurences(_url);

            Error = _headerAnalyzer.ErrorMsg;
            
            var model = new AnalyzeModel()
            {
                Keywords = _headerAnalyzer.Keywords,
                Results = occurences
            };

            return model;
        }
    }
}
