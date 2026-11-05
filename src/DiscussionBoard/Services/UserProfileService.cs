using DiscussionBoard.Data;

namespace DiscussionBoard.Services
{
    public class UserProfileService :  IUserProfileService
    {
        private readonly ApplicationDbContext _context;

        public UserProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> IsDisplayNameAvailableAsync(string displayName, string? excludeUserId = null)
        {
            if (displayName.Trim().Length > 1 || displayName.Trim().Length < 60)
            {
                bool isAvailable = _context.Users
                    .Any(u => u.DisplayName.Equals(displayName, StringComparison.CurrentCulture));
                return Task.FromResult(isAvailable);
            }

            return Task.FromResult(false);
        }
    }
}
