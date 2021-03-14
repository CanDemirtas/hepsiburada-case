using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DaikinProject.Models;
using HepsiburadaCase.Models;
using HepsiburadaCase.Service.Abstract;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HepsiburadaCase.Controllers {

    [Route ("api/[controller]")]
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly ICampaignAlgorithmService _campaignAlgorithmService;

        public HomeController (ILogger<HomeController> logger, ICampaignAlgorithmService campaignAlgorithmService) {
            _logger = logger;
            _campaignAlgorithmService = campaignAlgorithmService;
        }

        [HttpPost ("[action]")]
        public IActionResult GetProduct ([FromBody] ProductViewModel model) {
            var serviceResult = _campaignAlgorithmService.GetProduct (model);

            if (serviceResult.ResultType == ServiceResultType.Fail)
                return BadRequest (serviceResult.Message);

            return Ok (serviceResult.Data);
        }

        [HttpPost ("[action]")]
        public IActionResult CreateProduct ([FromBody] ProductViewModel model) {
            var serviceResult = _campaignAlgorithmService.CreateProduct (model);

            if (serviceResult.ResultType == ServiceResultType.Fail)
                return BadRequest (serviceResult.Message);

            return Ok (serviceResult.Data);
        }

        [HttpPost ("[action]")]
        public IActionResult CreateOrder ([FromBody] OrderViewModel model) {
            var serviceResult = _campaignAlgorithmService.CreateOrder (model);

            if (serviceResult.ResultType == ServiceResultType.Fail)
                return BadRequest (serviceResult.Message);

            return Ok (serviceResult.Data);
        }

        [HttpPost ("[action]")]
        public IActionResult CreateCampaign ([FromBody] CampaignViewModel model) {
            var serviceResult = _campaignAlgorithmService.CreateCampaign (model);

            if (serviceResult.ResultType == ServiceResultType.Fail)
                return BadRequest (serviceResult.Message);

            return Ok (serviceResult.Data);
        }

        [HttpPost ("[action]")]
        public IActionResult GetCampaign ([FromBody] CampaignViewModel model) {
            var serviceResult = _campaignAlgorithmService.GetCampaign (model);

            if (serviceResult.ResultType == ServiceResultType.Fail)
                return BadRequest (serviceResult.Message);

            return Ok (serviceResult.Data);
        }

        [HttpPost ("[action]")]
        public IActionResult IncreaseTime ([FromBody] int hour) {
            var serviceResult = _campaignAlgorithmService.IncreaseTime (hour);

            if (serviceResult.ResultType == ServiceResultType.Fail)
                return BadRequest (serviceResult.Message);

            return Ok (serviceResult.Data);
        }

        public IActionResult Index () {

            return View ();
        }

        public IActionResult Privacy () {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}