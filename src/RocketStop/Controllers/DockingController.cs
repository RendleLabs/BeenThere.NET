using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using RocketStop.Data;
using RocketStop.Models;

namespace RocketStop.Controllers
{
    [Route("docking")]
    public class DockingController : Controller
    {
        private readonly ILogger _logger;
        private readonly IDockingService _dockingService;
        private readonly IBayService _bayService;

        public DockingController(IDockingService dockingService, IBayService bayService, ILogger<DockingController> logger)
        {
            _dockingService = dockingService;
            _bayService = bayService;
            _logger = logger;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dockingService.Delete(id).ConfigureAwait(false);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _dockingService.List().ConfigureAwait(false));
        }

        [HttpGet("new")]
        public async Task<IActionResult> New()
        {
            var bays = await _bayService.List().ConfigureAwait(false);

            return View(new NewDockingViewModel
            {
                Bays = bays
                    .Select(b => new SelectListItem {
                        Value = b.Id,
                        Text = $"Bay {b.Id} ({b.Size}km)"}
                        ).ToList()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Docking docking)
        {
            await _dockingService.Add(docking).ConfigureAwait(false);
            return RedirectToAction("Index");
        }
    }
}