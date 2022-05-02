using System.Text.Json;
using Api.Exceptions;
using Api.Models;
using Api.Models.Entities;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("posts")]
public class PostsController : ControllerBase
{
    private readonly DbContext _dbContext;
    private readonly IContactsService _contactsService;

    public PostsController(DbContext dbContext, IContactsService contactsService)
    {
        _dbContext = dbContext;
        _contactsService = contactsService;
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

        var post = new Post
        {
            Id = postInDb.Id,
            Content = postInDb.Content,
            Title = postInDb.Title,
            Created = postInDb.Created,
            Updated = postInDb.Updated,
            Tags = JsonSerializer.Deserialize<IEnumerable<string>>(postInDb.TagsJson)!
        };

        return post with { Contact = postInDb.ContactId != null ? await GetContact(postInDb.ContactId.Value) : null };
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async IAsyncEnumerable<Post> GetPosts()
    {
        var postsInDb = await _dbContext.Posts.ToListAsync();
        
        foreach (var postEntity in postsInDb)
        {
            yield return new Post
            {
                Id = postEntity.Id,
                Content = postEntity.Content,
                Title = postEntity.Title,
                Created = postEntity.Created,
                Updated = postEntity.Updated,
                Contact = postEntity.ContactId != null ? await GetContact(postEntity.ContactId.Value) : null,
                Tags = JsonSerializer.Deserialize<IEnumerable<string>>(postEntity.TagsJson)!
            };
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePost(SavePostRequest request)
    {
        Contact? contactInfo = null;
        if (request.ContactId != null)
        {
            contactInfo = await GetContact(request.ContactId.Value);
            if (contactInfo == null)
            {
                return ReturnContactNotFoundBadRequest();
            }
        }
        
        var entity = new PostEntity
        {
            Content = request.Content, 
            Title = request.Title,
            TagsJson = request.Tags != null ? JsonSerializer.Serialize(request.Tags) : "[]",
            ContactId = request.ContactId
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
                Contact = contactInfo,
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
        
        Contact? contactInfo = null;
        if (post.ContactId != null)
        {
            contactInfo = await GetContact(post.ContactId.Value);
            if (contactInfo == null)
            {
                return ReturnContactNotFoundBadRequest();
            }
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
            Contact = contactInfo,
            Created = postInDb.Created,
            Updated = postInDb.Updated,
            Tags = JsonSerializer.Deserialize<IEnumerable<string>>(postInDb.TagsJson)!
        };
    }
    
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Post>> PatchPost(int id, [FromBody] PatchPostRequest request)
    {
        var postInDb = await _dbContext.Posts.FindAsync(id);
        if (postInDb == null)
        {
            return NotFound();
        }
        
        var saveRequest = new SavePostRequest
        {
            ContactId = postInDb.ContactId,
            Content = postInDb.Content, 
            Title = postInDb.Title,
            Tags = JsonSerializer.Deserialize<IEnumerable<string>>(postInDb.TagsJson)
        };

        if (request.ContactId != null)
        {
            saveRequest = saveRequest with { ContactId = request.ContactId };
        }
        
        if (request.Content != null)
        {
            saveRequest = saveRequest with { Content = request.Content };
        }

        if (request.Title != null)
        {
            saveRequest = saveRequest with { Title = request.Title };
        }

        if (request.Tags != null)
        {
            saveRequest = saveRequest with { Tags = request.Tags };
        }
        
        if (!TryValidateModel(saveRequest))
        {
            return BadRequest(new ValidationProblemDetails(ModelState));
        }
        
        Contact? contactInfo = null;
        if (request.ContactId != null)
        {
            contactInfo = await GetContact(request.ContactId.Value);
            if (contactInfo == null)
            {
                return ReturnContactNotFoundBadRequest();
            }
        }

        postInDb.ContactId = saveRequest.ContactId;
        postInDb.Content = saveRequest.Content;
        postInDb.Title = saveRequest.Title;
        postInDb.TagsJson = JsonSerializer.Serialize(saveRequest.Tags);
        await _dbContext.SaveChangesAsync();
        return new Post
        {
            Id = postInDb.Id, 
            Content = postInDb.Content,
            Title = postInDb.Title,
            Created = postInDb.Created,
            Updated = postInDb.Updated,
            Contact = request.ContactId == null && postInDb.ContactId != null ? await GetContact(postInDb.ContactId.Value) : contactInfo,
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

    private BadRequestObjectResult ReturnContactNotFoundBadRequest()
    {
        ModelState.Clear();
        ModelState.AddModelError("ContactId", "Contact not found");
        return BadRequest(new ValidationProblemDetails(ModelState));
    }
    
    private async Task<Contact?> GetContact(int contactId)
    {
        try
        {
            return await _contactsService.GetContact(contactId);
        } catch (ContactNotFoundException)
        {
            return null;
        }
    }
}