using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

[ApiController]
[Route("api/sets")]
public class SetsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SetsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult LogSets([FromBody] Set set)
    {

        if (set == null || set.ExerciseId <= 0 || set.TrainingEventId <= 0)
        {
            return BadRequest("Invalid set data");
        }

        var exercise = _context.Exercises.Find(set.ExerciseId);
        var trainingEvent = _context.TrainingEvents.Find(set.TrainingEventId);

        if (exercise == null || trainingEvent == null)
        {
            return NotFound("Exercise or TrainingEvent not found");
        }

        _context.Sets.Add(set);
        _context.SaveChanges();

        return Ok(set);
    }

    [HttpGet("exercise/{exerciseId}")]
    public IActionResult GetSetsByExercise(int exerciseId)
    {

        var sets = _context.Sets.Where(s => s.ExerciseId == exerciseId).ToList();

        if (sets == null || sets.Count == 0)
        {
            return NotFound("No sets found for the exercise");
        }

        return Ok(sets);
    }

    [HttpGet("trainingEvent/{trainingEventId}")]
    public IActionResult GetSetsByTrainingEvent(int trainingEventId)
    {

        var sets = _context.Sets.Where(s => s.TrainingEventId == trainingEventId).ToList();

        if (sets == null || sets.Count == 0)
        {
            return NotFound("No sets found for the training event");
        }

        return Ok(sets);
    }
}

