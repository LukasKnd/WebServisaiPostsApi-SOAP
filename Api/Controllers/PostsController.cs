using System.Text.Json;
using Api.Models;
using Api.Models.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("posts")]
public class PostsController : ControllerBase
{
    private readonly DbContext _dbContext;

    public PostsController(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{id}", Name = "GetPost")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var postInDb = await _dbContext.Posts.FindAsync(id);
        if (postInDb == null)
        {
            return NotFound();
        }

        return new Post
        {
            Id = postInDb.Id, 
            Content = postInDb.Content, 
            Title = postInDb.Title, 
            Created = postInDb.Created,
            Updated = postInDb.Updated,
            Tags = JsonSerializer.Deserialize<IEnumerable<string>>(postInDb.TagsJson)!
        };
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<Post>> GetPosts()
    {
        return (await _dbContext.Posts.ToListAsync()).Select(x =>
            new Post
            {
                Id = x.Id,
                Content = x.Content,
                Title = x.Title,
                Created = x.Created,
                Updated = x.Updated,
                Tags = JsonSerializer.Deserialize<IEnumerable<string>>(x.TagsJson)!
            });
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePost(SavePostRequest request)
    {
        var entity = new PostEntity
        {
            Content = request.Content, 
            Title = request.Title,
            TagsJson = request.Tags != null ? JsonSerializer.Serialize(request.Tags) : "[]"
        };
        _dbContext.Posts.Add(entity);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction("GetPost", new { entity.Id },
            new Post
            {
                Id = entity.Id, 
                Content = entity.Content, 
                Title = entity.Title, 
                Created = entity.Created,
                Updated = entity.Updated,
                Tags = JsonSerializer.Deserialize<IEnumerable<string>>(entity.TagsJson)!
            });
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Post>> UpdatePost(int id, SavePostRequest post)
    {
        var postInDb = await _dbContext.Posts.FindAsync(id);
        if (postInDb == null)
        {
            return NotFound();
        }
        postInDb.Content = post.Content!;
        postInDb.Title = post.Title!;
        postInDb.TagsJson = post.Tags != null ? JsonSerializer.Serialize(post.Tags) : "[]";
        
        await _dbContext.SaveChangesAsync();
        return new Post
        {
            Id = postInDb.Id, 
            Content = postInDb.Content,
            Title = postInDb.Title,
            Created = postInDb.Created,
            Updated = postInDb.Updated,
            Tags = JsonSerializer.Deserialize<IEnumerable<string>>(postInDb.TagsJson)!
        };
    }
    
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Post>> PatchPost(int id, [FromBody] JsonPatchDocument<SavePostRequest> patchDoc)
    {
        var postInDb = await _dbContext.Posts.FindAsync(id);
        if (postInDb == null)
        {
            return NotFound();
        }

        var prefillData = new SavePostRequest
        {
            Content = postInDb.Content, 
            Title = postInDb.Title,
            Tags = JsonSerializer.Deserialize<IEnumerable<string>>(postInDb.TagsJson)
        };
        patchDoc.ApplyTo(prefillData, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!TryValidateModel(prefillData))
        {
            return BadRequest(ModelState);
        }

        postInDb.Content = prefillData.Content;
        postInDb.Title = prefillData.Title;
        postInDb.TagsJson = JsonSerializer.Serialize(prefillData.Tags);
        await _dbContext.SaveChangesAsync();
        return new Post
        {
            Id = postInDb.Id, 
            Content = postInDb.Content,
            Title = postInDb.Title,
            Created = postInDb.Created,
            Updated = postInDb.Updated,
            Tags = JsonSerializer.Deserialize<IEnumerable<string>>(postInDb.TagsJson)!
        };
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePost(int id)
    {
        var postInDb = await _dbContext.Posts.FindAsync(id);
        if (postInDb == null)
        {
            return NotFound();
        }

        _dbContext.Posts.Remove(postInDb);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}