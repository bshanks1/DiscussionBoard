using DiscussionBoard.Models;

namespace DiscussionBoard.Services
{
    public interface IPostService
    {
        public Task<List<Post>> GetForThreadAsync(int threadId);
        public Task<Post> AddAsync(Post post);
    }
}
