using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CupcakeApi.Models;

namespace CupcakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CupcakeItemsController : ControllerBase
    {
        private readonly CupcakeContext _context;

        public CupcakeItemsController(CupcakeContext context)
        {
            _context = context;
        }

        // GET: api/CupcakeItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CupcakeItem>>> GetCupcakeItems()
        {
          if (_context.CupcakeItems == null)
          {
              return NotFound();
          }
            return await _context.CupcakeItems.ToListAsync();
        }

        // GET: api/CupcakeItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CupcakeItem>> GetCupcakeItem(long id)
        {
          if (_context.CupcakeItems == null)
          {
              return NotFound();
          }
            var cupcakeItem = await _context.CupcakeItems.FindAsync(id);

            if (cupcakeItem == null)
            {
                return NotFound();
            }

            return cupcakeItem;
        }

        // PUT: api/CupcakeItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCupcakeItem(long id, CupcakeItem cupcakeItem)
        {
            if (id != cupcakeItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(cupcakeItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CupcakeItemExists(id))
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

        // POST: api/CupcakeItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CupcakeItem>> PostCupcakeItem(CupcakeItem cupcakeItem)
        {
          if (_context.CupcakeItems == null)
          {
              return Problem("Entity set 'CupcakeContext.CupcakeItems'  is null.");
          }
            _context.CupcakeItems.Add(cupcakeItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCupcakeItem), new { id = cupcakeItem.Id }, cupcakeItem);
        }

        // DELETE: api/CupcakeItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCupcakeItem(long id)
        {
            if (_context.CupcakeItems == null)
            {
                return NotFound();
            }
            var cupcakeItem = await _context.CupcakeItems.FindAsync(id);
            if (cupcakeItem == null)
            {
                return NotFound();
            }

            _context.CupcakeItems.Remove(cupcakeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CupcakeItemExists(long id)
        {
            return (_context.CupcakeItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
