using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim_System.Models
{
    public class ClaimsModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
