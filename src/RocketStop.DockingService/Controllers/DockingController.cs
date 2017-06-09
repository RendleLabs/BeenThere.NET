using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketStop.DockingService.Data;
using RocketStop.DockingService.Models;

namespace RocketStop.DockingService.Controllers
{
    [Route("api")]
    public class DockingController
    {
        private readonly DockingContext _context;

        public DockingController(DockingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var all = await _context.Dockings.ToListAsync();
            return new OkObjectResult(all);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var docking = await _context.Dockings.FindAsync(id);
            if (docking == null) return new StatusCodeResult(404);
            return new OkObjectResult(docking);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Docking docking)
        {
            await _context.Dockings.AddAsync(docking);
            await _context.SaveChangesAsync();
            return new CreatedAtActionResult("Get", "Docking", new {id = docking.Id}, docking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var docking = await _context.Dockings.FindAsync(id);
            if (docking == null) return new StatusCodeResult(404);
            _context.Dockings.Remove(docking);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}
