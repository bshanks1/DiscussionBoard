using DiscussionBoard.Data;
using DiscussionBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscussionBoard.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PostService> _logger;

        public PostService(ApplicationDbContext context) => _context = context;

        public async Task<List<Post>> GetForThreadAsync(int threadId)
        {
            var posts = _context.Posts
                .Where(t => t.DiscussionThreadId == threadId);
            return await posts.ToListAsync();
        }

        public async Task<Post> AddAsync(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
