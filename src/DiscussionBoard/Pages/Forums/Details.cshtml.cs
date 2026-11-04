using DiscussionBoard.Models;
using DiscussionBoard.Services;
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

        public Details(IForumService forums, IDiscussionThreadService threads, ILogger<Details> logger)
        {
            _forums = forums;
            _threads = threads;
            _logger = logger;
        }

        public void OnGet()
        {
            forum = _forums.GetAsync(forumid).GetAwaiter().GetResult();
            DiscussionThread lastThread = new DiscussionThread() {Id = -13112, Title = "blank", Content = "blank"};

            foreach (DiscussionThread thread in _threads.GetForForumAsync(forumid).GetAwaiter().GetResult())
            {
                if (thread.Id == lastThread.Id)
                {
                    continue;
                }
                lastThread = thread;
                forum.DiscussionThreads.Add(thread);
            }
        }
    }
}
