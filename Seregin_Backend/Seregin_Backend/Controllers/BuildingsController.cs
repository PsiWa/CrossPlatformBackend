using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seregin_Backend.Data;
using Seregin_Backend.Models;

namespace Seregin_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly BuildingContext _context;

        public BuildingsController(BuildingContext context)
        {
            _context = context;
        }

        // GET: api/Buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildings()
        {
            return await _context.Buildings.ToListAsync();
        }

        // GET: api/Buildings/byUser/5
        [HttpGet("byUser/{user_id}")]
        public async Task<ActionResult> GetBuildingsByUserId(int user_id)
        {
            var user = await _context.Users
                .Include(u=>u.Projects)
                .ThenInclude(p=>p.Apt)
                .FirstOrDefaultAsync(u=>u.UserID==user_id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Projects.Select(p=>p.Apt.ToString()));
        }
        // GET: api/Buildings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(int id)
        {
            var building = await _context.Buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        [HttpGet("test/{id}")]
        public async Task<ActionResult<Building>> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return await _context.Buildings
                .Include(a => a.Apts)
                .ThenInclude(p => p.ProjectsForApt)
                .ThenInclude(u => u.Usr)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.BuildingID == id);
        }

        // PUT: api/Buildings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(int id, Building building)
        {
            if (id != building.BuildingID)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Buildings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilding", new { id = building.BuildingID }, building);
        }

        // DELETE: api/Buildings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Building>> DeleteBuilding(int id)
        {
            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();

            return building;
        }

        private bool BuildingExists(int id)
        {
            return _context.Buildings.Any(e => e.BuildingID == id);
        }
    }
}
