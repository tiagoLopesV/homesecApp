using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/entities")]
[ApiController]
public class EntityController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EntityController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Entity>>> GetEntities()
    {
        return await _context.Entities.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Entity>> AddEntity(Entity entity)
    {
        _context.Entities.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEntities), new { id = entity.id }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEntity(int id, Entity entity)
    {
        if (id != entity.id) return BadRequest("Entity ID mismatch.");

        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEntity(int id)
    {
        var entity = await _context.Entities.FindAsync(id);
        if (entity == null)
        {
            return NotFound();
        }

        try
        {
            _context.Entities.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (DbUpdateException)
        {
            return Conflict("Cannot delete this entity because it is referenced by other records.");
        }
    }
}
