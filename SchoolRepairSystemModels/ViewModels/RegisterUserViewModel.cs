using System.ComponentModel.DataAnnotations;

namespace SchoolRepairSystemModels.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, StringLength(20)]
        public string UserName { get; set; }
        [Required, StringLength(20)]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
    }
}