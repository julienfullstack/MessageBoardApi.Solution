using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoardApi.Models;

namespace MessageBoardApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PostsController : ControllerBase
  {
    private readonly MessageBoardApiContext _db;

    public PostsController(MessageBoardApiContext db)
    {
      _db = db;
    }

    // GET api/Post
    [HttpGet]
    public async Task<List<Posts>> Get(int postid, int userid, int groupid, string name, string message, string datetime)
    {
      IQueryable<Posts> query = _db.Post.AsQueryable();

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      }

      if (message != null)
      {
        query = query.Where(entry => entry.Message == message);
      }

      if (datetime != null)
      {
        query = query.Where(entry => entry.DateTime == datetime);
      }

      return await query.ToListAsync();
    }

    // GET: api/Post/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Posts>> GetPost(int id)
    {
      Posts Posts = await _db.Post.FindAsync(id);

      if (Posts == null)
      {
        return NotFound();
      }

      return Posts;
    }

    // POST api/Post
    [HttpPost]
    public async Task<ActionResult<Posts>> Post([FromBody] Posts Posts)
    {
      _db.Post.Add(Posts);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetPost), new { id = Posts.PostId }, Posts);
    }

        // PUT: api/Post/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Posts Posts)
    {
      if (id != Posts.PostId)
      {
        return BadRequest();
      }

      _db.Post.Update(Posts);

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
      return _db.Post.Any(e => e.PostId == id);
    }

    // DELETE: api/Post/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
      Posts Posts = await _db.Post.FindAsync(id);
      if (Posts == null)
      {
        return NotFound();
      }

      _db.Post.Remove(Posts);
      await _db.SaveChangesAsync();

      return NoContent();
    }
  }
}