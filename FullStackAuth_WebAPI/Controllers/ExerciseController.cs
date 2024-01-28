using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/exercises")]
public class ExerciseController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ExerciseController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateExercise([FromBody] Exercise exercise)
    {
        _context.Exercises.Add(exercise);
        _context.SaveChanges();
        return Ok(exercise);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateExercise(int id, [FromBody] Exercise updatedExercise)
    {
        var existingExercise = _context.Exercises.Find(id);

        if (existingExercise == null)
        {
            return NotFound();
        }

        existingExercise.Type = updatedExercise.Type;
        existingExercise.Weight = updatedExercise.Weight;
        existingExercise.Reps = updatedExercise.Reps;
        existingExercise.Sets = updatedExercise.Sets;

        _context.SaveChanges();

        return Ok(existingExercise);
    }

    [HttpGet("{id}")]
    public IActionResult GetExercise(int id)
    {
        var exercise = _context.Exercises.Find(id);

        if (exercise == null)
        {
            return NotFound();
        }

        return Ok(exercise);
    }
}
