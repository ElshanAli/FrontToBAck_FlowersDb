using System.ComponentModel.DataAnnotations;

namespace FrontToBackFlowers.ViewModels
{
    public class RegisterViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmedPassword { get; set; }
    }
}
