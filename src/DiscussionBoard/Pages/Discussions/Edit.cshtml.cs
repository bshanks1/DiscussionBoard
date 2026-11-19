using System.Security.Claims;
using DiscussionBoard.Authorization;
using DiscussionBoard.Models;
using DiscussionBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscussionBoard.Pages.Discussions
{
    [Authorize]
    public class Edit : PageModel
    {
        [BindProperty]
        public DiscussionEditViewModel Input { get; set; }
        [BindProperty(SupportsGet = true)]
        public int threadid { get; set; }
        public DiscussionThread Thread { get; set; }
        private readonly IDiscussionThreadService ThreadService;
        private readonly IApplicationAuthorizationService ApplicationAuthorizationService;

        public Edit(IDiscussionThreadService _threadService, IApplicationAuthorizationService applicationAuthorizationService)
        {
            ThreadService = _threadService;
            Input = new  DiscussionEditViewModel();
            ApplicationAuthorizationService = applicationAuthorizationService;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            if (ThreadService.GetAsync(threadid).Result == null)
            {
                return NotFound();
            }

            Thread = ThreadService.GetAsync(threadid).Result;

            if (ApplicationAuthorizationService.CanEditAsync(User, Thread).Result)
            {
                return Page();
            }

            return Forbid();
        }

        public async Task<IActionResult> OnPost()
        {
            Thread = ThreadService.GetAsync(threadid).Result;

            if (ApplicationAuthorizationService.CanEditAsync(User, Thread).Result)
            {

                if (Input.Title.Length > 160 || Input.Content.Length > 1000 || Input.Title.Length < 3 ||
                    Input.Content.Length < 1)
                {
                    RedirectToPage("/Error");
                }

                else
                {
                    Thread.Title = Input.Title;
                    Thread.Content = Input.Content;

                    ThreadService.UpdateAsync(Thread);
                }
            }

            return Forbid();
        }
    }
}
