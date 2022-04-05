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
    public class UsersController : Controller
    {
        private readonly BuildingContext _context;

        public UsersController(BuildingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsersById(int id)
        {
            return Ok(await _context.Users.FindAsync(id));
        }

        [HttpGet("GetProjectsForRoomN/{n}/{name}")]
        public async Task<ActionResult> GetProjectsForRoomN(int n, string name)
        {
            //var apartments = await _context.Apartments
            //    .Include(a => a.ProjectsForApt)
            //    .ThenInclude(p => p.Usr).Where(a => a.RoomsN == n);
            var projects = _context.DesignProjects
                .Include(p => p.Apt)
                .ThenInclude(a => a.Bldng)
                .Where(p => p.Apt.RoomsN == n)
                .Include(p => p.Usr)
                .Where(p => p.Usr.UName == name);
            string Result = "";
            foreach (DesignProject pr in projects)
                Result = Result + pr.ToString()+"\n";
            return Ok(Result);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", user);
        }

        [HttpPost("Project/{user_id}/{aprtment_id}")]
        public async Task<ActionResult<DesignProject>> PostProject(DesignProject project)
        {
            if (project == null)
            {
                return NotFound();
            }
            _context.DesignProjects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.Include(u => u.Projects).FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

    }
}
