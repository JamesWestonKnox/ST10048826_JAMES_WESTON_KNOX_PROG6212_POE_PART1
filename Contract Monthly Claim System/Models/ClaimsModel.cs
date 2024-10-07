//James Knox
//ST10048826
//GROUP 3
//References:
//Bootstrap, n.d. CSS. [Online] Available at: https://getbootstrap.com/docs/3.4/css/#tables [Accessed 9 September 2024].
//OpenAI.2024.Chat-GPT (Version 3.5).[Large language model].Available at: https://chat.openai.com/ [Accessed: 8 September 2024]

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Contract_Monthly_Claim_System.Models
{
    public class ClaimsModel
    {
        public ClaimsModel() { }
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string ModuleCode { get; set; }

        [Required]
        public string MonthOfClaim { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Hours worked must be a positive number.")]
        public double HoursWorked { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Hourly rate must be a positive number.")]
        public decimal HourlyRate { get; set; }


        [Required]
        public string SupportingDocumentPath { get; set; }

    }
}

