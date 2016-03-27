using System;
using System.Web;
using System.Web.Http;
using BLL.Abstract;
using BLL.Models;
using Lab1_des.Models;
using Models;

namespace Lab1_des.ApiControllers
{
    public class MessageController : BaseApiController
    {
        private readonly IMessageService _messageService;
        private readonly IPushNotificationService _notificationService;

        public MessageController(IMessageService messageService, IPushNotificationService notificationService)
        {
            _messageService = messageService;
            _notificationService = notificationService;
        }

        [HttpGet]
        [Route("api/test/{email}/send/{text}")]
        public IHttpActionResult SendMessage(string email, string text)
        {
            _notificationService.Notify(new NotificationDto
            {
                Header = "Новое сообщение",
                Id = 1,
                FromId = "-1",
                RedirectLink = "",
                Text = text,
                UserId = email
            });

            return Ok();
        }

        [HttpPost]
        [Route("api/message/send")]
        public IHttpActionResult SendMessage(CreateMessageViewModel message)
        {
            var messageEntity = new Message
            {
                Date = DateTime.UtcNow,
                FromId = CurrentUserEmail,
                ToId = message.Id,
                Text = message.Text
            };
            _messageService.SendMessage(messageEntity);
            _notificationService.Notify(new NotificationDto
            {
                Header = "Новое сообщение",
                Id = 1,
                RedirectLink = "/" + CurrentUserEmail + "/chat",
                OriginText = message.Text,
                Text = "Вам пришло сообщение от " + CurrentUserEmail,
                UserId = message.Id,
                FromId = CurrentUserEmail
            });

            return Ok();
        }
    }
}
