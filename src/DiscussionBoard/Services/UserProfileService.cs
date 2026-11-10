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
            if (nameValidLength(displayName))
            {
               if (excludeUserId == null)
               {
                    bool isAvailable = _context.Users
                        .Any(u => u.DisplayName.ToLower().Trim().Equals(displayName.ToLower().Trim()));
                    return Task.FromResult(!isAvailable);
               }
               else
               {
                    bool isAvailable = _context.Users.Any(u => u.DisplayName.ToLower().Trim().Equals(displayName.ToLower().Trim())
                                                               && u.Id != excludeUserId);
                    return Task.FromResult(!isAvailable);
               }
            }

            return Task.FromResult(false);
        }

        public bool nameValidLength(string displayName)
        {
            if (displayName != null && displayName.Trim().Length > 1 && displayName.Trim().Length < 61)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
