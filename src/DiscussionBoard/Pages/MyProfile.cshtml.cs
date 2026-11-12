using DiscussionBoard.Models;
using DiscussionBoard.Services;
using DiscussionBoard.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DiscussionBoard.Pages
{
    public class MyProfile : PageModel
    {
        public UserManager<ApplicationUser> UserManager { get; set; }
        public IUserProfileService UserProfileService { get; set; }
        [BindProperty]
        public UserProfile Input { get; set; }
        public string NoUserMessage { get; set; }
        public string NameAvailabilityMessage { get; set; }
        public string AvatarAvailableMessage { get; set; }
        private bool canUpdateProfile = true;

        public MyProfile(UserManager<ApplicationUser> userManager, IUserProfileService userProfileService)
        {
            UserManager = userManager;
            UserProfileService = userProfileService;
        }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            return Page();
        }

        public void OnPost()
        {
            if (UserManager.GetUserAsync(User).Result != null)
            {
                if (Input.DisplayName != null && UserProfileService.IsDisplayNameAvailableAsync(Input.DisplayName, UserManager.GetUserAsync(User).Result.Id).Result)
                {
                    UserManager.GetUserAsync(User).Result.DisplayName = Input.DisplayName.Trim();
                }

                else if (Input.DisplayName != null && UserProfileService.nameValidLength(Input.DisplayName) == false)
                {
                    NameAvailabilityMessage = "Name Not Valid, ensure it is between 2-60 characters";
                    canUpdateProfile = false;
                }

                else if (Input.DisplayName != null && UserProfileService.nameValidLength(Input.DisplayName))
                {
                    NameAvailabilityMessage = "Name Unavailable";
                    canUpdateProfile = false;
                }

                else if (Input.DisplayName.IsWhiteSpace())
                {
                    NameAvailabilityMessage = "Please enter a name";
                    canUpdateProfile = false;
                }


                if (Input.AvatarId != null && AvatarCatalog.IsValid(Input.AvatarId) && canUpdateProfile)
                {
                    UserManager.GetUserAsync(User).Result.AvatarId = Input.AvatarId;
                }

                else if (Input.AvatarId != null && Input.AvatarId.IsWhiteSpace() ||
                         AvatarCatalog.IsValid(Input.AvatarId) == false)
                {
                    AvatarAvailableMessage = "Invalid Section";
                    canUpdateProfile = false;
                }

                else if (Input.AvatarId == null)
                {
                    AvatarAvailableMessage = "Please enter a valid avatar id";
                    canUpdateProfile = false;
                }

                if (canUpdateProfile)
                {
                    UserManager.UpdateAsync(UserManager.GetUserAsync(User).Result).Wait();

                    if (Input.DisplayName != null)
                    {
                        NameAvailabilityMessage = "Display Name Changed";
                    }

                    if (Input.AvatarId != null)
                    {
                        AvatarAvailableMessage = "Avatar Changed";
                    }
                }
            }
        }
    }
}
