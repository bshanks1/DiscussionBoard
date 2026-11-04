using DiscussionBoard.Models;

namespace DiscussionBoard.Services
{
    public interface IForumService
    {
        public Task<List<Forum>> GetAllAsync();
        public Task<Forum> GetAsync(int id);
    }
}
