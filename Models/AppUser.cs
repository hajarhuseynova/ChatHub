using Microsoft.AspNetCore.Identity;

namespace ChatApp.Models
{
    public class AppUser:IdentityUser
    {
        public string? ConnectionId { get; set; }    


    }
}
