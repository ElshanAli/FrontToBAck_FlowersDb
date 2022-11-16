using Microsoft.AspNetCore.Identity;

namespace FrontToBackFlowers.Models.IdentityModels
{
    public class IdentityOfUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
