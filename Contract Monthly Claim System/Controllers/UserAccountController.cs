using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claim_System.Controllers
{
    public class UserAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
