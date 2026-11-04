using DiscussionBoard.Models;

namespace DiscussionBoard.Services
{
    public interface IDiscussionThreadService
    {
        public Task<DiscussionThread> GetAsync(int id);
        public Task<List<DiscussionThread>> GetForForumAsync(int forumId);
        public Task<DiscussionThread> AddAsync(DiscussionThread thread);
        public Task<DiscussionThread> UpdateAsync(DiscussionThread thread);

    }
}
