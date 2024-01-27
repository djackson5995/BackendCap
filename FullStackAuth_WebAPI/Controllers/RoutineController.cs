using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
}
