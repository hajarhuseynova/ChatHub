using ChatApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Context
{
    public class ChatDbContext:IdentityDbContext<AppUser>
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> option):base(option)
        {

        }
    }
}
