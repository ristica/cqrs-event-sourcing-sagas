using System.ComponentModel.DataAnnotations;

namespace BankAccount.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [MinLength(4)]
        [Display(Name = "Employee ID")]
        public string EmployeeId { get; set; }

        [Required]
        [MinLength(4)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string Name { get; set; }
    }
}
