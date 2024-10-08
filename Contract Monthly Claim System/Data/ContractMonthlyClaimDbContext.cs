using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contract_Monthly_Claim_System.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Contract_Monthly_Claim_System.Data
{
    public class ContractMonthlyClaimDbContext : IdentityDbContext<ApplicationUser>
    {
        public ContractMonthlyClaimDbContext(DbContextOptions<ContractMonthlyClaimDbContext> options) : base(options)
        {

        }

        public DbSet<ClaimsModel> Claims { get; set; }
    }
}
