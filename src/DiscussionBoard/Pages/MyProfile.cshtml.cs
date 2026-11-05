using DiscussionBoard.Models;
using DiscussionBoard.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscussionBoard.Pages
{
    public class MyProfile : PageModel
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public UserProfile Profile { get; set; }

        public MyProfile(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public void OnGet()
        {
            Profile = new UserProfile()
            {

            }
        }
    }
}
