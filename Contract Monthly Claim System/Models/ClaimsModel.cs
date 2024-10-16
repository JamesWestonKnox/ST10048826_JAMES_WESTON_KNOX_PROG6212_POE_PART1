//James Knox
//ST10048826
//GROUP 3
//References:
//Bootstrap, n.d. CSS. [Online] Available at: https://getbootstrap.com/docs/3.4/css/#tables [Accessed 9 September 2024].
//OpenAI.2024.Chat-GPT (Version 3.5).[Large language model].Available at: https://chat.openai.com/ [Accessed: 8 September 2024]

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contract_Monthly_Claim_System.Models
{
    public class ClaimsModel
    {
        [Key]
        public int ClaimID { get; set; }
        public int UserID   { get; set; }

        [ForeignKey("UserID")] // Specify the foreign key relationship
        public virtual UserModel User { get; set; } // Navigation property
        public string ModuleCode { get; set; }
        public DateTime? DateOfClaim { get; set; }
        public double HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string DocumentPath { get; set; }

        public string DocumentName { get; set; }
        public string ClaimStatus { get; set; } = "Pending";
    }
}

