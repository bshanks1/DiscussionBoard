using DiscussionBoard.Data;
using DiscussionBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscussionBoard.Services
{
    public class ForumService : IForumService
    {
        private readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context) => _context = context;
        public async Task<List<Forum>> GetAllAsync()
        {
            var forums = _context.Forums
                .Where(f => f.IsDeleted == false);
            return await forums.ToListAsync();
        }

        public async Task<Forum> GetAsync(int id)
        {
            return await _context.Forums
                .SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}
