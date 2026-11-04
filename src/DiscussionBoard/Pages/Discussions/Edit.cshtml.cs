using System.Security.Claims;
using DiscussionBoard.Models;
using DiscussionBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscussionBoard.Pages.Discussions
{
    public class Edit : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty(SupportsGet = true)]
        public int threadid { get; set; }
        public DiscussionThread Thread { get; set; }
        private readonly ILogger _logger;
        private readonly IDiscussionThreadService ThreadService;

        public Edit(IDiscussionThreadService _threadService, ILogger<Create> logger)
        {
            _logger = logger;
            ThreadService = _threadService;
            Input = new InputModel();
        }

        public void OnGet()
        {
            Thread = ThreadService.GetAsync(threadid).Result;
        }

        public void OnPost()
        {
            Thread = ThreadService.GetAsync(threadid).Result;

            if (Input.Title.Length > 500 || Input.Content.Length > 500 || Input.Title.Length <= 0 ||
                Input.Content.Length <= 0)
            {
                _logger.LogError("Title and Content must be between 1 and 500 characters");
                RedirectToPage("/Error");
            }

            else
            {
                Thread.Title = Input.Title;
                Thread.Content = Input.Content;

                ThreadService.UpdateAsync(Thread);
            }
        }

        public class InputModel
        {
            public string Title { get; set; }
            public string Content { get; set; }
        }

    }
}
