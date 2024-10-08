﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contract_Monthly_Claim_System.Models;

namespace Contract_Monthly_Claim_System.Data
{
    public class ContractMonthlyClaimDbContext : DbContext
    {
        public ContractMonthlyClaimDbContext(DbContextOptions<ContractMonthlyClaimDbContext> options) : base(options)
        {

        }


        public DbSet<UserModel> Users { get; set; }
        public DbSet<ClaimsModel> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().Property(m => m.PasswordHash).IsRequired(false);
            base.OnModelCreating(modelBuilder);
        }
    }


}
