// Controllers/EmpleadosController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CASTOR_API.Data;
using CASTOR_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class EmpleadosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EmpleadosController(AppDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<IActionResult> CreateEmpleado([FromForm] Empleado empleado, IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            var filePath = Path.Combine("wwwroot/images", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            empleado.FotoRuta = $"/images/{file.FileName}";
        }

        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        return Ok(empleado);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> GetEmpleados()
    {
        var empleados = await _context.Empleados
            .Select(e => new EmpleadoDto
            {
                Id = e.Id,
                Cedula = e.Cedula,
                Nombre = e.Nombre,
                FotoRuta = e.FotoRuta,
                FechaIngreso = e.FechaIngreso,
                Cargo = e.CargoId != null ? new CargoDto
                {
                    IdCargo = e.CargoId.Value,
                    Nombre = _context.Cargos
                        .Where(c => c.IdCargo == e.CargoId)
                        .Select(c => c.Nombre)
                        .FirstOrDefault()
                } : null
            })
            .ToListAsync();

        return Ok(empleados);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmpleado(int id, [FromForm] Empleado empleado, IFormFile? file)
    {
       /* if (id != empleado.Id)
        {
            return BadRequest();
        }*/

        var existingEmpleado = await _context.Empleados.FindAsync(id);
        if (existingEmpleado == null)
        {
            return NotFound();
        }

        existingEmpleado.Nombre = empleado.Nombre;
        existingEmpleado.Cedula = empleado.Cedula;
        existingEmpleado.FechaIngreso = empleado.FechaIngreso;
        existingEmpleado.CargoId = empleado.CargoId;

        if (file != null)
        {
            var newFileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine("wwwroot/images", newFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            existingEmpleado.FotoRuta = $"/images/{file.FileName}"; ;
        }

        _context.Entry(existingEmpleado).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmpleado(int id)
    {
        var empleado = await _context.Empleados.FindAsync(id);
        if (empleado == null)
        {
            return NotFound();
        }

        _context.Empleados.Remove(empleado);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
