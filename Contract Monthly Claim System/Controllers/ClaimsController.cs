//James Knox
//ST10048826
//GROUP 3
//References:

//Bootstrap, n.d. CSS. [Online] Available at: https://getbootstrap.com/docs/3.4/css/#tables [Accessed 9 September 2024].
//OpenAI.2024.Chat-GPT (Version 3.5).[Large language model].Available at: https://chat.openai.com/ [Accessed: 8 September 2024]

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Contract_Monthly_Claim_System.Data;
using Contract_Monthly_Claim_System.Models;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Identity.Client;

namespace Contract_Monthly_Claim_System.Controllers
{
    public class ClaimsController : BaseController
    {
        private readonly ContractMonthlyClaimDbContext _context;

        public ClaimsController(ContractMonthlyClaimDbContext context)
        {
            _context = context;
        }
        public IActionResult UserDashboard()
        {
            var userID = ViewBag.UserID as int?;

            var claims = _context.Claims
                             .Where(c => c.UserID == userID)
                             .ToList();

            ViewBag.Claims = claims;
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitClaim(ClaimsModel model, IFormFile DocumentPath)
        {
            ModelState.Remove("DocumentPath");
            ModelState.Remove("DocumentName");
            ModelState.Remove("User");

            if (!ModelState.IsValid)
            {
                // Log or inspect what is wrong with ModelState
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); // You can replace this with proper logging
                }

                return View("UserDashboard", model);
            }

            var userID = HttpContext.Session.GetInt32("UserID");
            if (userID == null)
            {
                // Handle the case where the user is not logged in or session expired
                return RedirectToAction("Login", "UserAccount");
            }

            model.UserID = (int)userID;

            if (DocumentPath != null && DocumentPath.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/documents", DocumentPath.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    DocumentPath.CopyTo(stream);
                }

                model.DocumentPath = filePath;
                model.DocumentName = DocumentPath.FileName;
            }

            _context.Claims.Add(model);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the database save failure
                return View("UserDashboard", model);
            }

            return RedirectToAction("UserDashboard");
        }
    }
}
