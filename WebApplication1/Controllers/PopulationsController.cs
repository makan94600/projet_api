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
    public class PopulationsController : ControllerBase
    {
        private readonly TodoContext _context;

        public PopulationsController(TodoContext context)
        {
            _context = context;
        }





        //Avoir la population d'un pays d'une année donnée
        // GET: api/Populations/year
        [HttpGet("year/{searchedYear}/{searchedPays}")]
        public async Task<ActionResult<IEnumerable<Population>>> PoppulationByYear(string searchedYear,string searchedPays)
        {
            var population = from m in _context.Population
                         select m;

            if (!String.IsNullOrEmpty(searchedYear))
            {
                if (!String.IsNullOrEmpty(searchedPays))
                {
                    population = population.Include(o => o.pays2).Include(p=>p.Individus).Where(k => k.pays2.nom!.Contains(searchedPays));
                }
                population = population.Where(s => s.annee!.Contains(searchedYear));
            }

            return await population.ToListAsync();
        }



        //Avoir la population d'un continent d'une année donnée
        // GET: api/Populations/year2
        [HttpGet("year2/{searchedYear}/{searchedContinent}")]
        public async Task<ActionResult<IEnumerable<Population>>> PoppulationByYearC(string searchedYear, string searchedContinent)
        {
            var population = from m in _context.Population
                             select m;

            if (!String.IsNullOrEmpty(searchedYear))
            {
                if (!String.IsNullOrEmpty(searchedContinent))
                {
                    population = population.Include(o => o.pays2).Include(p => p.Individus).Where(k => k.pays2.continent !.Contains(searchedContinent));
                }
                population = population.Where(s => s.annee!.Contains(searchedYear));
            }

            return await population.ToListAsync();
        }



        // GET: api/Populations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Population>>> GetPopulation()
        {
            return await _context.Population.Include(o => o.pays2).Include(p => p.Individus).ToListAsync();
        }

        // GET: api/Populations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Population>> GetPopulation(int id)
        {
            var population = await _context.Population.FindAsync(id);

            if (population == null)
            {
                return NotFound();
            }

            return population;
        }

        // PUT: api/Populations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPopulation(int id, Population population)
        {
            if (id != population.Id)
            {
                return BadRequest();
            }

            _context.Entry(population).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PopulationExists(id))
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

        // POST: api/Populations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Population>> PostPopulation(Population population)
        {
            _context.Population.Add(population);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPopulation", new { id = population.Id }, population);
        }

        // DELETE: api/Populations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Population>> DeletePopulation(int id)
        {
            var population = await _context.Population.FindAsync(id);
            if (population == null)
            {
                return NotFound();
            }

            _context.Population.Remove(population);
            await _context.SaveChangesAsync();

            return population;
        }

        private bool PopulationExists(int id)
        {
            return _context.Population.Any(e => e.Id == id);
        }
    }
}
