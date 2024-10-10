using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contract_Monthly_Claim_System.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }

        [NotMapped]
        public string Password { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }

        public virtual ICollection<ClaimsModel> Claims { get; set; } = new List<ClaimsModel>();
    }
}
