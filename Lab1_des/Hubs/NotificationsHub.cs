using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BLL.Models;
using Lab1_des.Models;
using Microsoft.AspNet.SignalR;

namespace Lab1_des.Hubs
{
    public class NotificationsHub : Hub
    {
        public static readonly ConcurrentDictionary<string, NotificationUser> Users
            = new ConcurrentDictionary<string, NotificationUser>();

        public override Task OnConnected()
        {
            var userId = (Context.User as ClaimsPrincipal).FindFirst(ClaimTypes.NameIdentifier).Value;
            var connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(
                userId,
                new NotificationUser
                {
                    Id = userId,
                    ConnectionIds = new ConcurrentDictionary<string, byte>()
                });

            user.ConnectionIds.TryAdd(connectionId, 0);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userId = (Context.User as ClaimsPrincipal).FindFirst(ClaimTypes.NameIdentifier).Value;
            var connectionId = Context.ConnectionId;

            NotificationUser user;
            if (Users.TryGetValue(userId, out user))
            {
                byte b;
                user.ConnectionIds.TryRemove(connectionId, out b);

                if (!user.ConnectionIds.Any())
                {
                    Users.TryRemove(userId, out user);
                }
            }

            return base.OnDisconnected(stopCalled);
        }

        public void Notify(NotificationDto notification)
        {
            var connectionIds = Users[notification.UserId].ConnectionIds;

            Clients.Clients(connectionIds.Keys.ToList()).Notify(notification);
        }
    }
}