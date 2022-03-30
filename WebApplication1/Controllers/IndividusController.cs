using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividusController : ControllerBase
    {
        private readonly TodoContext _context;

        public IndividusController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/Individus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Individu>>> GetIndividu()
        {
            return await _context.Individu.ToListAsync();
        }

        // GET: api/Individus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Individu>> GetIndividu(int id)
        {
            var individu = await _context.Individu.FindAsync(id);

            if (individu == null)
            {
                return NotFound();
            }

            return individu;
        }

        // PUT: api/Individus/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndividu(int id, Individu individu)
        {
            if (id != individu.Id)
            {
                return BadRequest();
            }

            _context.Entry(individu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndividuExists(id))
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

        // POST: api/Individus
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Individu>> PostIndividu(Individu individu)
        {
            _context.Individu.Add(individu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIndividu", new { id = individu.Id }, individu);
        }

        // DELETE: api/Individus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Individu>> DeleteIndividu(int id)
        {
            var individu = await _context.Individu.FindAsync(id);
            if (individu == null)
            {
                return NotFound();
            }

            _context.Individu.Remove(individu);
            await _context.SaveChangesAsync();

            return individu;
        }

        private bool IndividuExists(int id)
        {
            return _context.Individu.Any(e => e.Id == id);
        }
    }
}
