using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/trainingevents")]
public class TrainingEventController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TrainingEventController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateTrainingEvent([FromBody] TrainingEvent trainingEvent)
    {
        // Ensure that the associated routine exists
        var existingRoutine = _context.Routines.Find(trainingEvent.RoutineId);
        if (existingRoutine == null)
        {
            return NotFound("Routine not found");
        }

        _context.TrainingEvents.Add(trainingEvent);
        _context.SaveChanges();

        return Ok(trainingEvent);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTrainingEvent(int id, [FromBody] TrainingEvent updatedTrainingEvent)
    {
        var existingTrainingEvent = _context.TrainingEvents.Find(id);

        if (existingTrainingEvent == null)
        {
            return NotFound();
        }

        existingTrainingEvent.Date = updatedTrainingEvent.Date;
        existingTrainingEvent.RoutineId = updatedTrainingEvent.RoutineId;
        existingTrainingEvent.UserId = updatedTrainingEvent.UserId;

        _context.SaveChanges();

        return Ok(existingTrainingEvent);
    }

    [HttpGet("{id}")]
    public IActionResult GetTrainingEvent(int id)
    {
        var trainingEvent = _context.TrainingEvents.Find(id);

        if (trainingEvent == null)
        {
            return NotFound();
        }

        return Ok(trainingEvent);
    }

    [HttpGet]
    public IActionResult GetAllTrainingEvents()
    {
        var trainingEvents = _context.TrainingEvents.Include(te => te.Routine).ToList();

        return Ok(trainingEvents);
    }
}
