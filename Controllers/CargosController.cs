// Controllers/CargosController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CASTOR_API.Data;
using CASTOR_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class CargosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CargosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cargo>>> GetCargos()
    {
        return await _context.Cargos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cargo>> GetCargo(int id)
    {
        var cargo = await _context.Cargos.FindAsync(id);

        if (cargo == null)
        {
            return NotFound();
        }

        return cargo;
    }

    [HttpPost]
    public async Task<ActionResult<Cargo>> PostCargo(Cargo cargo)
    {
        _context.Cargos.Add(cargo);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCargo", new { id = cargo.IdCargo }, cargo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCargo(int id, Cargo cargo)
    {
        if (id != cargo.IdCargo)
        {
            return BadRequest();
        }

        _context.Entry(cargo).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCargo(int id)
    {
        var cargo = await _context.Cargos.FindAsync(id);
        if (cargo == null)
        {
            return NotFound();
        }

        _context.Cargos.Remove(cargo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
