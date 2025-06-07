using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/divisions")]
[ApiController]
public class DivisionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DivisionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all divisions with entity details
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAllDivisions()
    {
        var divisions = await _context.Divisions
            .Include(d => d.Entity)
            .Select(d => new
            {
                id = d.id,
                name = d.name,
                entityId = d.entityId,
                entityName = d.Entity != null ? d.Entity.name : "Unknown"
            })
            .ToListAsync();

        return Ok(divisions);
    }

    // Get divisions by entityId
[HttpGet("entity/{entityId}")]
public async Task<ActionResult<IEnumerable<object>>> GetDivisionsByEntity([FromRoute] int entityId)
{
    Console.WriteLine($"Received entityId: {entityId}"); // Debugging line

    if (entityId <= 0) return BadRequest("Invalid entity ID.");

   var divisions = await _context.Divisions
    .Include(d => d.Entity) // Ensure Entity is included
    .Where(d => d.entityId == entityId)
    .Select(d => new
    {
        id = d.id,
        name = d.name,
        entityId = d.entityId
    })
    .ToListAsync();


    if (!divisions.Any()) return NotFound("No divisions found for the given entity.");

    return Ok(divisions);
}


    // Add a new division
    [HttpPost]
    public async Task<ActionResult<Division>> AddDivision(Division division)
    {
        if (string.IsNullOrEmpty(division.name) || division.entityId == 0)
        {
            return BadRequest("Name and Entity ID are required.");
        }

        var entity = await _context.Entities.FindAsync(division.entityId);
        if (entity == null)
        {
            return BadRequest("Invalid Entity ID.");
        }

        _context.Divisions.Add(division);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDivisionsByEntity), new { id = division.id }, division);
    }

    // Update a division
[HttpPut("{id}")]
public async Task<IActionResult> UpdateDivision(int id, [FromBody] Division updatedDivision)
{
    if (id != updatedDivision.id) 
        return BadRequest("Division ID mismatch.");

    var existingDivision = await _context.Divisions.FindAsync(id);
    if (existingDivision == null)
        return NotFound("Division not found.");

    // Update fields
    existingDivision.name = updatedDivision.name;
    existingDivision.entityId = updatedDivision.entityId; // Allow updating entityId

    await _context.SaveChangesAsync();

    return NoContent();
}


    // Delete a division
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDivision(int id)
    {
        var division = await _context.Divisions.FindAsync(id);
        if (division == null) return NotFound();

        _context.Divisions.Remove(division);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
