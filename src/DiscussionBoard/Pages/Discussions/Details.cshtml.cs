using DiscussionBoard.Models;
using DiscussionBoard.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscussionBoard.Pages.Discussions
{
    public class Details : PageModel
    {
        private readonly ILogger<Details> _logger;
        private readonly IDiscussionThreadService _threadService;
        private readonly IPostService _postService;
        [BindProperty (SupportsGet = true)]
        public int threadId { get; set; }
        public DiscussionThread Thread { get; set; }
        public UserManager<ApplicationUser> UserManager { get; }

        public List<PostTreeBuilder.Node> nodes;

        public Details(ILogger<Details> logger, IDiscussionThreadService threadService, IPostService postService,  UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _threadService = threadService;
            _postService = postService;
            UserManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            Thread = _threadService.GetAsync(threadId).Result;
            nodes = PostTreeBuilder.Build(_postService.GetForThreadAsync(threadId).Result, UserManager);
            return Page();
        }
    }
}

