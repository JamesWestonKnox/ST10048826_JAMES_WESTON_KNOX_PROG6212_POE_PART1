//James Knox
//ST10048826
//GROUP 3
//References:
//Bootstrap, n.d. CSS. [Online] Available at: https://getbootstrap.com/docs/3.4/css/#tables [Accessed 9 September 2024].
//OpenAI.2024.Chat-GPT (Version 3.5).[Large language model].Available at: https://chat.openai.com/ [Accessed: 8 September 2024]

using Contract_Monthly_Claim_System.Data;
using Contract_Monthly_Claim_System.Hashing;
using Contract_Monthly_Claim_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim_System.Controllers
{
    public class UserAccountController : BaseController
    {
        private readonly ContractMonthlyClaimDbContext _context;

        public UserAccountController(ContractMonthlyClaimDbContext context)
        {
            _context = context;
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
        public IActionResult Register(UserModel model)
        {
            ModelState.Remove("PasswordHash");

            if (!ModelState.IsValid)
            {
                // Log the validation errors to the console for debugging
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return View(model); // Return to the view so you can see the form again
            }

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
                return View(model);
            }

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email is already taken");
                return View(model);
            }

            var user = new UserModel
            {
                FirstName = model.FirstName,
                Surname = model.Surname,
                Email = model.Email,
                PasswordHash = PasswordHasher.HashPassword(model.Password),
                Role = model.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login", "UserAccount");
        }

        [HttpPost]
        public IActionResult Login(LoginModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);
                if (user != null && PasswordHasher.VerifyPassword(model.Password, user.PasswordHash))
                {
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserRole", user.Role);
                    HttpContext.Session.SetInt32("UserID", user.UserID);
                    HttpContext.Session.SetString("UserName", user.FirstName + " " +  user.Surname);

                    return RedirectToAction("Index", "Home"); // Redirect to home page
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(model);
        }
    }
}