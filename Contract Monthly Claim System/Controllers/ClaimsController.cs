using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim_System.Controllers
{
    public class ClaimsController : Controller
    {
        public IActionResult UserDashboard()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}
