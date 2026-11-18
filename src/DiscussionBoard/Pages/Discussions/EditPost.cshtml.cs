using DiscussionBoard.Authorization;
using DiscussionBoard.Models;
using DiscussionBoard.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscussionBoard.Pages.Discussions;

[Authorize]
public class EditPost : PageModel
{
    [BindProperty (SupportsGet = true)]
    public DiscussionEditViewModel Input { get; set; }
    [BindProperty (SupportsGet = true)]
    public int threadid { get; set; }
    [BindProperty (SupportsGet = true)]
    public int postid { get; set; }
    public DiscussionThread Thread { get; set; }
    public Post  Post { get; set; }
    public IDiscussionThreadService ThreadService { get; set; }
    public IPostService PostService { get; set; }
    public IApplicationAuthorizationService ApplicationAuthorizationService { get; set; }

    public EditPost(IDiscussionThreadService threadService,  IPostService postService, IApplicationAuthorizationService authorizationService)
    {
        ThreadService = threadService;
        PostService = postService;
        ApplicationAuthorizationService = authorizationService;
    }

    public async Task<IActionResult> OnGet()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return Challenge();
        }

        if (ThreadService.GetAsync(threadid).Result == null || PostService.GetAsync(postid).Result == null)
        {
            return NotFound();
        }

        Thread = ThreadService.GetAsync(threadid).Result;
        Post = PostService.GetAsync(postid).Result;

        if (ApplicationAuthorizationService.CanEditAsync(User, Thread).Result)
        {
            return Page();
        }

        return Forbid();
    }

    public void OnPost()
    {
        if (Thread.Id == threadid && Post.Id == postid && ApplicationAuthorizationService.CanEditAsync(User, Post).Result)
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
    }
}
