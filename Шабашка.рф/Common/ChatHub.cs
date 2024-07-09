using Microsoft.AspNetCore.SignalR;

namespace Шабашка.рф.Common
{
    public class ChatHub : Hub
    {
        public async Task Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            await Clients.All.SendAsync("addNewMessageToPage", name, message);
        }
    }
}
