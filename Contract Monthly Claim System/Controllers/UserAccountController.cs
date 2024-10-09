//James Knox
//ST10048826
//GROUP 3
//References:
//Bootstrap, n.d. CSS. [Online] Available at: https://getbootstrap.com/docs/3.4/css/#tables [Accessed 9 September 2024].
//OpenAI.2024.Chat-GPT (Version 3.5).[Large language model].Available at: https://chat.openai.com/ [Accessed: 8 September 2024]

using Contract_Monthly_Claim_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim_System.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public UserAccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    Surname = model.Surname,
                    Role = model.Role
                };

                // Create the user
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign role to the user
                    await _userManager.AddToRoleAsync(user, model.Role);

                    // Redirect to the login page after successful registration
                    return RedirectToAction("Login", "UserAccount");
                }

                // If user creation failed, add errors to the ModelState and re-display the form
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If we get here, something went wrong, re-display the form with errors
            return View(model);
        }
    }
}
