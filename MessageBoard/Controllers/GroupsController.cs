using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoard.Models;

namespace MessageBoard.Controllers
{
  [Route("api/[Controller]")]
  [ApiController]
  public class GroupsController : ControllerBase
  {
    private readonly MessageBoardContext _db;
    public GroupsController(MessageBoardContext db)
    {
      _db = db;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> Get(string groupDescription, string groupName)
    {
      var query = _db.Groups.AsQueryable();
      if(groupDescription != null)
      {
        query = query.Where(entry => entry.GroupDescription == groupDescription);
      }
      if(groupName != null)
      {
        query = query.Where(entry => entry.GroupName == groupName);
      }
      return await query.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> GetGroup(int id)
    {
      var group = await _db.Groups.FindAsync(id);
      if(group == null)
      {
        return NotFound();
      }
      return group;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Group group)
    {
      if (id != group.GroupId)
      {
        return BadRequest();
      }
      _db.Entry(group).State = EntityState.Modified;
      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!GroupExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Group>> Post(Group group)
    {
      _db.Groups.Add(group);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetGroup), new { id = group.GroupId }, group);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
      var group = await _db.Groups.FindAsync(id);
      if (group == null)
      {
        return NotFound();
      }

      _db.Groups.Remove(group);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool GroupExists(int id)
    {
      return _db.Groups.Any(e => e.GroupId == id);
    }
  }
}