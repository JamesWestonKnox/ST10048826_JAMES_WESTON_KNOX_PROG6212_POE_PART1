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
using Microsoft.EntityFrameworkCore.Migrations;

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

            var claims = _context.Claims.Where(c => c.UserID == userID).ToList();

            ViewBag.Claims = claims;
            return View();
        }

        public IActionResult AdminDashboard()
        {
            var pendingClaims = _context.Claims.Where(c => c.ClaimStatus == "Pending").Include(c => c.User).ToList();

            var pastClaims = _context.Claims?.Where(c => c.ClaimStatus != "Pending").Include(c => c.User).ToList();

            ViewBag.PendingClaims = pendingClaims;

            ViewBag.PastClaims = pastClaims;

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
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage); 
                }

                return View("UserDashboard", model);
            }

            var userID = HttpContext.Session.GetInt32("UserID");
            if (userID == null)
            {
                return RedirectToAction("Login", "UserAccount");
            }

            model.UserID = (int)userID;

            if (DocumentPath != null && DocumentPath.Length > 0)
            {
                if (DocumentPath.Length > 5000000)
                {
                    ModelState.AddModelError("", "File exceeds size limit");
                    return View("UserDashboard", model);
                }

                var validFileTypes = new[] { ".pdf", ".docx", ".xlsx" };
                var fileType = Path.GetExtension(DocumentPath.FileName).ToLower();

                if (!validFileTypes.Contains(fileType)) 
                {
                    ModelState.AddModelError("", "Invalid file type.");
                    return View("UserDashboard", model); ;
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Documents");

                var filePath = Path.Combine(uploadFolder, DocumentPath.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    DocumentPath.CopyTo(stream);
                }

                model.DocumentPath = filePath;
                model.DocumentName = DocumentPath.FileName;
            }
            else
            {
                model.DocumentPath = null;
                model.DocumentName = null;
            }

            _context.Claims.Add(model);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("UserDashboard", model);
            }

            return RedirectToAction("UserDashboard");
        }


        [HttpPost]
        public IActionResult UpdateClaimStatus(int claimID, string status)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimID == claimID);
            if (claim != null)
            {
                claim.ClaimStatus = status;
                _context.SaveChanges();
            }

            return RedirectToAction("AdminDashboard");
        }

        public IActionResult ViewDocument (string fileName)
        {
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Documents");

            var filePath = Path.Combine(uploadFolder, fileName);

            var fileType = Path.GetExtension(fileName).ToLower();
            var contentType = fileType switch
            {
                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, contentType, fileName);

        }
    }
}
