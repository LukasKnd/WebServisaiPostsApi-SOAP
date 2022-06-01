using System.Text.Json;
using Api.Exceptions;
using Api.Models;
using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PostsService: IPostsService
{
    private readonly DbContext _dbContext;
    private readonly IContactsService _contactsService;

    public PostsService(DbContext dbContext, IContactsService contactsService)
    {
        _dbContext = dbContext;
        _contactsService = contactsService;
    }
    
    public async Task<List<Post>> GetPosts()
    {
        var postsInDb = await _dbContext.Posts.ToListAsync();

        var posts = new List<Post>();
        foreach (var postEntity in postsInDb)
        {
            posts.Add(new Post
            {
                Id = postEntity.Id,
                Content = postEntity.Content,
                Title = postEntity.Title,
                Created = postEntity.Created,
                Updated = postEntity.Updated,
                Contact = postEntity.ContactId != null ? await GetContact(postEntity.ContactId.Value) : null,
                Tags = JsonSerializer.Deserialize<IEnumerable<string>>(postEntity.TagsJson)!
            });
        }

        return posts;
    }
    
    public async Task<Post> GetPost(int id)
    {
        var postInDb = await _dbContext.Posts.FindAsync(id);
        if (postInDb == null)
        {
            throw new Exception("Contact not found");
        }

        return new Post
        {
            Id = postInDb.Id,
            Content = postInDb.Content,
            Title = postInDb.Title,
            Created = postInDb.Created,
            Updated = postInDb.Updated,
            Tags = JsonSerializer.Deserialize<IEnumerable<string>>(postInDb.TagsJson)!,
            Contact = postInDb.ContactId != null ? await GetContact(postInDb.ContactId.Value) : null
        };
    }
    
    public async Task<Post> CreatePost(SavePostRequest request)
    {
        var contactInfo = await ProcessContact(request);

        var entity = new PostEntity
        {
            Content = request.Content, 
            Title = request.Title,
            TagsJson = request.Tags != null ? JsonSerializer.Serialize(request.Tags) : "[]",
            ContactId = contactInfo?.Id
        };
        _dbContext.Posts.Add(entity);
        await _dbContext.SaveChangesAsync();
        return new Post
        {
            Id = entity.Id,
            Content = entity.Content,
            Title = entity.Title,
            Created = entity.Created,
            Updated = entity.Updated,
            Contact = contactInfo,
            Tags = JsonSerializer.Deserialize<IEnumerable<string>>(entity.TagsJson)!
        };
    }
    
    public async Task<Post> UpdatePost(int id, SavePostRequest post)
    {
        var postInDb = await _dbContext.Posts.FindAsync(id);
        if (postInDb == null)
        {
            throw new Exception("Contact not found");
        }
        
        var contactInfo = await ProcessContact(post);

        postInDb.Content = post.Content!;
        postInDb.Title = post.Title!;
        postInDb.TagsJson = post.Tags != null ? JsonSerializer.Serialize(post.Tags) : "[]";
        postInDb.ContactId = contactInfo?.Id;
        
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
    
    public async Task DeletePost(int id)
    {
        var postInDb = await _dbContext.Posts.FindAsync(id);
        if (postInDb == null)
        {
            throw new Exception("Contact not found");
        }

        _dbContext.Posts.Remove(postInDb);
        await _dbContext.SaveChangesAsync();
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
    
    private async Task<Contact?> ProcessContact(IWithContactRequest request)
    {
        Contact? contactInfo = null;
        if (request.ContactId != null)
        {
            try
            {
                contactInfo = await _contactsService.GetContact(request.ContactId.Value);
            }
            catch (ContactNotFoundException)
            {
                throw new Exception("ContactId: " + request.ContactId + " was not found");
            }
        } else if (request.Contact != null)
        {
            try
            {
                if (request.Contact.Id != null)
                {
                    contactInfo = await _contactsService.UpdateContact(request.Contact);
                }
                else
                {
                    contactInfo = await _contactsService.CreateContact(request.Contact);
                }
            }
            catch (ContactNotFoundException)
            {
                throw new Exception("Contact with id: " + request.Contact.Id + " was not found");
            }
        }

        return contactInfo;
    }
}