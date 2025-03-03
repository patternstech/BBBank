using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Services
{
    public class UpdateHub : Hub
    {
        private static readonly ConcurrentDictionary<string, string> _connections = new();
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                _connections[userId] = Context.ConnectionId;
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                _connections.TryRemove(userId, out _);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendUpdate(string userId, string message)
        {

            if (_connections.TryGetValue(userId, out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("updateGraphsData", message);
            }

        }
    }
}
