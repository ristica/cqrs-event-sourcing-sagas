using System.ComponentModel.DataAnnotations;

namespace BankAccount.ApplicationLayer.Models
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "Employee ID")]
        public string EmployeeId { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string Name { get; set; }
    }
}
