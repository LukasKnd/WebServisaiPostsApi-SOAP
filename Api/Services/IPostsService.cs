using System.ServiceModel;
using Api.Models;

namespace Api.Services;

[ServiceContract(Name = "PostsApi", Namespace = "http://www.example.com/posts")]
public interface IPostsService
{
    [OperationContract]
    Task<List<Post>> GetPosts();

    [OperationContract]
    Task<Post> GetPost(int id);

    [OperationContract]
    Task<Post> CreatePost(SavePostRequest request);

    [OperationContract]
    Task<Post> UpdatePost(int id, SavePostRequest post);

    [OperationContract]
    Task DeletePost(int id);
}