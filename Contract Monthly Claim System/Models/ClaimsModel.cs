//James Knox
//ST10048826
//GROUP 3
//References:
//Bootstrap, n.d. CSS. [Online] Available at: https://getbootstrap.com/docs/3.4/css/#tables [Accessed 9 September 2024].
//OpenAI.2024.Chat-GPT (Version 3.5).[Large language model].Available at: https://chat.openai.com/ [Accessed: 8 September 2024]

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
