using Microsoft.AspNetCore.Identity;

namespace Contract_Monthly_Claim_System.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string Surname { get; set; }

        public string Role {  get; set; }


    }
}
