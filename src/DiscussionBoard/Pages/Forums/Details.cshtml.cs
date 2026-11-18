using DiscussionBoard.Authorization;
using DiscussionBoard.Models;
using DiscussionBoard.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscussionBoard.Pages.Forums
{
    public class Details : PageModel
    {
        [BindProperty(SupportsGet = true)] public int forumid { get; set; }

        public Forum forum { get; set; }

        private readonly IForumService _forums;
        private readonly IDiscussionThreadService _threads;
        private readonly ILogger<Details> _logger;
        public UserManager<ApplicationUser> userManager;
        public IApplicationAuthorizationService ApplicationAuthorizationService { get; }

        public Details(IForumService forums, IDiscussionThreadService threads, ILogger<Details> logger, UserManager<ApplicationUser> userManager,  IApplicationAuthorizationService applicationAuthorizationService)
        {
            _forums = forums;
            _threads = threads;
            _logger = logger;
            this.userManager = userManager;
            ApplicationAuthorizationService = applicationAuthorizationService;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }
            else
            {
                forum = _forums.GetAsync(forumid).GetAwaiter().GetResult();

                foreach (DiscussionThread thread in _threads.GetForForumAsync(forumid).GetAwaiter().GetResult())
                {
                    forum.DiscussionThreads.Add(thread);
                }
            }

            return Page();
        }
    }
}
