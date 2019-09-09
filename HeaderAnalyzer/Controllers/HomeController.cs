using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HeaderAnalyzer.Models;
using HeaderAnalyzer.ViewModelBuilders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tools.Interfaces;

namespace HeaderAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHeaderAnalyzer _headerAnalyzer;

        public HomeController(ILogger<HomeController> logger, IHeaderAnalyzer headerAnalyzer)
        {
            _logger = logger;
            _headerAnalyzer = headerAnalyzer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new HomeModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeModel model)
        {
            var modelBuilder = new AnalyzeModelBuilder(model.Url, _headerAnalyzer);
            model.AnalyzeResult = await modelBuilder.Build();
            model.Error = modelBuilder.Error;

            return View(model);
        }
    }
}
