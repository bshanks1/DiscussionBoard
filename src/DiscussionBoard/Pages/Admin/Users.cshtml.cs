using DiscussionBoard.Authorization;
using DiscussionBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscussionBoard.Pages.Admin
{
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class Users : PageModel
    {
        public UserManager<ApplicationUser> userManager { get; set; }
        public List<ApplicationUser> users { get; set; } = new List<ApplicationUser>();
        public ApplicationUser SelectedUser { get; set; }
        [BindProperty]
        public string selectedUserId { get; set; }
        public bool canHaveRole { get; set; }
        public bool isUserSelected { get; set; }

        public Users(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;

            foreach (var user in userManager.Users)
            {
                users.Add(user);
            }
        }

        public void OnGet()
        {
            isUserSelected = false;
        }

        public void OnPost()
        {
            isUserSelected = true;
            SelectedUser = userManager.FindByIdAsync(selectedUserId).Result;
        }

        public void OnPostUpdateRoleAsync()
        {
            SelectedUser = userManager.FindByIdAsync(selectedUserId).Result;
        }
    }
}
