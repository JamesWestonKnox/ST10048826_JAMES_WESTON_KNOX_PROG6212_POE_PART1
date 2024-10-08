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

namespace Contract_Monthly_Claim_System.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ContractMonthlyClaimDbContext _context;

        public ClaimsController(ContractMonthlyClaimDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> UserDashboard()
        {
            return View(await _context.Claims.ToListAsync());
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }

        
    }
}
