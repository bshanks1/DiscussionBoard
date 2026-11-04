using DiscussionBoard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscussionBoard.Pages;

public class IndexModel : PageModel
{
    public IServiceProvider ServiceProvider { get; set; }

    public IndexModel(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }
    public void OnGet()
    {
        //DevelopmentSeedService.SeedAsync(ServiceProvider).Wait();
    }
}
