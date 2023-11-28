using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoardApi.Models;

namespace MessageBoardApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GroupsController : ControllerBase
  {
    private readonly MessageBoardApiContext _db;

    public GroupsController(MessageBoardApiContext db)
    {
      _db = db;
    }

    // GET api/Group
    [HttpGet]
    public async Task<List<Groups>> Get(int groupid, string name)
    {
      IQueryable<Groups> query = _db.Group.AsQueryable();

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }

      return await query.ToListAsync();
    }

    // GET: api/Group/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Groups>> GetGroup(int id)
    {
      Groups Groups = await _db.Group.FindAsync(id);

      if (Groups == null)
      {
        return NotFound();
      }

      return Groups;
    }

    // Group api/Group
    [HttpPost]
    public async Task<ActionResult<Groups>> Group([FromBody] Groups Groups)
    {
      _db.Group.Add(Groups);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetGroup), new { id = Groups.GroupId }, Groups);
    }

        // PUT: api/Group/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Groups Groups)
    {
      if (id != Groups.GroupId)
      {
        return BadRequest();
      }

      _db.Group.Update(Groups);

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PostExists(id))
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

    private bool PostExists(int id)
    {
      return _db.Group.Any(e => e.GroupId == id);
    }

    // DELETE: api/Group/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
      Groups Groups = await _db.Group.FindAsync(id);
      if (Groups == null)
      {
        return NotFound();
      }

      _db.Group.Remove(Groups);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    [HttpGet("{id}/Posts")]
    public async Task<ActionResult<List<Posts>>> GetPostsByGroupId(int groupId)
    {
      var posts = await _db.Post.Where(p => p.GroupId == groupId).ToListAsync();
      return posts;
    }
  }

  }
