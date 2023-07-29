using ChatApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub:Hub
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;
        public ChatHub(IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager = null)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task SendMessage(string userId, string message)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
            string userName= _contextAccessor.HttpContext.User.Identity.Name;
                if (!string.IsNullOrWhiteSpace(user.ConnectionId))
                {
                    await Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage",userName, message);
                }
            }
        }

        public override async Task OnConnectedAsync()
        {
            string connid=Context.ConnectionId;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser= await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);  
                appUser.ConnectionId=connid;
                await _userManager.UpdateAsync(appUser);
                string userId = appUser.Id;
                await Clients.All.SendAsync("Loggin",userId);
            }
           await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {

            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
                appUser.ConnectionId = null;
                await _userManager.UpdateAsync(appUser);
                string userId = appUser.Id;
                await Clients.All.SendAsync("Logout", userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
