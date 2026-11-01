using DiscussionBoard.Data;
using DiscussionBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscussionBoard.Services
{
    public class ForumService
    {
        private readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context) => _context = context;
        public async Task<List<Forum>> GetAllAsync()
        {
            var forums = _context.Forums
                .Where(f => f.isDeleted == false);
            return await forums.ToListAsync();
        }
    }
}
