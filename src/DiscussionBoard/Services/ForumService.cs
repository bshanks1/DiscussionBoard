using DiscussionBoard.Data;
using DiscussionBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscussionBoard.Services
{
    public class ForumService
    {
        private readonly ApplicationDbContext _context;
        public async Task<List<Forum>> GetAllAsync()
        {
            var forums = _context.Forums
                .Where(c => c.IsDeleted == false)
                .Include(c => c.Name)
                .Include(c => c.Description);
            return await forums.ToListAsync();
        }
    }
}
