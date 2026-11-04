using DiscussionBoard.Data;
using DiscussionBoard.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace DiscussionBoard.Services
{
    public class DiscussionThreadService : IDiscussionThreadService
    {
        private readonly ApplicationDbContext _context;
        private ILogger _logger;

        public DiscussionThreadService(ApplicationDbContext context) => _context = context;


        public async Task<DiscussionThread> GetAsync(int id)
        {
            return await _context.DiscussionThreads
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<DiscussionThread>> GetForForumAsync(int forumId)
        {
            var threads = _context.DiscussionThreads
                .Where(t => t.ForumId == forumId);
                return await threads.ToListAsync();
        }

        public async Task<DiscussionThread> AddAsync(DiscussionThread thread)
        {
            _context.Add(thread);
            await _context.SaveChangesAsync();
            return thread;
        }

        public async Task<DiscussionThread> UpdateAsync(DiscussionThread thread)
        {
            _context.Update(thread);
            await _context.SaveChangesAsync();
            return thread;
        }
    }
}
