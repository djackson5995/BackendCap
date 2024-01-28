using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;

[Authorize]
[ApiController]
[Route("api/routines")]
public class RoutineController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RoutineController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateRoutine([FromBody] Routine routine)
    {
        _context.Routines.Add(routine);
        _context.SaveChanges();
        return Ok(routine);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRoutine(int id, [FromBody] Routine updatedRoutine)
    {
        var existingRoutine = _context.Routines.Find(id);

        if (existingRoutine == null)
        {
            return NotFound();
        }

        existingRoutine.Title = updatedRoutine.Title;
        existingRoutine.Description = updatedRoutine.Description;

        _context.SaveChanges();

        return Ok(existingRoutine);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoutines()
    {
        try
        {
            var routines = await _context.Routines.ToListAsync();
            return Ok(routines);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRoutine(int id)
    {
        try
        {
            var routine = _context.Routines.Find(id);
            if (routine == null)
            {
                return NotFound();
            }

            _context.Routines.Remove(routine);
            _context.SaveChanges();

            return Ok("Routine deleted successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
