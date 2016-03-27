using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Abstract;
using BLL.Models;
using Lab1_des.Hubs;
using Lab1_des.Models;
using Microsoft.AspNet.SignalR;

namespace Lab1_des.Infrastructure
{
    public class PushNotificationService : IPushNotificationService
    {
        public void Notify(NotificationDto notificationDto)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationsHub>();

            if (NotificationsHub.Users.ContainsKey(notificationDto.UserId))
            {
                var connectionIds = NotificationsHub.Users[notificationDto.UserId].ConnectionIds;
                hubContext.Clients.Clients(connectionIds.Keys.ToList()).notify(notificationDto);
            }
        }
    }
}