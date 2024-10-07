using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contract_Monthly_Claim_System.Models;

namespace Contract_Monthly_Claim_System.Data
{
    public class ContractMonthlyClaimDbContext : DbContext
    {
        public ContractMonthlyClaimDbContext(DbContextOptions<ContractMonthlyClaimDbContext> options) : base(options)
        {

        }

        public DbSet<ClaimsModel> Claims { get; set; }
    }
}
