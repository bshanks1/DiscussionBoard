using DiscussionBoard.Models;
using DiscussionBoard.Services;
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

        public List<PostTreeBuilder.Node> nodes;

        public Details(ILogger<Details> logger, IDiscussionThreadService threadService, IPostService postService)
        {
            _logger = logger;
            _threadService = threadService;
            _postService = postService;
        }

        public void OnGet()
        {
            Thread = _threadService.GetAsync(threadId).Result;
            nodes = PostTreeBuilder.Build(_postService.GetForThreadAsync(threadId).Result);
        }
    }
}

