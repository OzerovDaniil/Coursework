using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SushiManagementSystem.Application.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace SushiManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [Authorize]
        [HttpGet("inventory")]
        public async Task<IActionResult> GetInventoryReport()
        {
            var report = await _reportService.GetInventoryReportAsync();
            return Ok(report);
        }
    }
}