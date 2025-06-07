using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/sites")]
[ApiController]

public class SiteController : ControllerBase{
    private readonly ApplicationDbContext _context;

    public SiteController(ApplicationDbContext context){
        _context=context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAllSites(){
        var sites = await _context.Sites
            .Include(d => d.Division)
            .Select(d => new
            {
                id = d.id,
                name = d.name,
                divisionId = d.divisionId,
                divisonName = d.Division != null ? d.Division.name: "Unknown"
            })
            .ToListAsync();
        return Ok(sites);
    }

    [HttpGet("division/{divisionId}")]
    public async Task<ActionResult<IEnumerable<object>>> GetSitesByDivision([FromRoute] int divisionId){
        if (divisionId <= 0) return BadRequest("Invalid division ID.");

        var sites = await _context.Sites
            .Include(d => d.Division)
            .Where(d => d.divisionId == divisionId)
            .Select(d => new
            {
                id = d.id,
                name = d.name,
                divisionId = d.divisionId
            })
            .ToListAsync();

            if (!sites.Any()) return NotFound("No sites found for the given division.");

            return Ok(sites);
    }

    [HttpPost]
    public async Task<ActionResult<Site>> AddSite(Site site)
    {
        if (string.IsNullOrEmpty(site.name) || site.divisionId == 0){
            return BadRequest("Name and Division ID are required.");
        }

        var division = await _context.Divisions.FindAsync(site.divisionId);

        if (division == null){
            return BadRequest("Invalid Division ID.");
        }

        _context.Sites.Add(site);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSitesByDivision), new { divisionId = site.divisionId }, site);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateSite(int id, Site site){
        if (id != site.id) return BadRequest("Site ID mismatch.");

        _context.Entry(site).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSite(int id){
        var site = await _context.Sites.FindAsync(id);

        if(site == null) return NotFound();

        _context.Sites.Remove(site);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}