using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using BLL.Abstract;
using Lab1_des.Models;

namespace Lab1_des.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DesChat(string id)
        {
            var room = GetChatRoom(id);
            if (room == null)
            {
                return RedirectToAction("Index");
            }

            return View(room);
        }

        public ActionResult KeyChat(string id)
        {
            var room = GetChatRoom(id);
            if (room == null)
            {
                return RedirectToAction("Index");
            }

            return View(room);
        }

        public ActionResult RsaChat(string id)
        {
            var room = GetChatRoom(id);
            if (room == null)
            {
                return RedirectToAction("Index");
            }

            return View(room);
        }

        public ActionResult DiffyChat(string id)
        {
            var room = GetChatRoom(id);
            if (room == null)
            {
                return RedirectToAction("Index");
            }

            return View(room);
        }

        [HttpGet]
        public ActionResult CurrentUserPhoto()
        {
            return RedirectToAction("Photo", new { email = CurrentUserEmail });
        }

        [HttpGet]
        public ActionResult Photo(string email)
        {
            var user = _userService.GetUserOrDefault(email);
            var path = string.Format("{0}\\{1}", Server.MapPath("~/Content/assets/img"), user.Email);

            return File(path, user.ContentType);
        }

        [HttpGet]
        public PartialViewResult UserList()
        {
            var users = _userService.GetUsers().Where(u => u.Email != CurrentUserEmail);
            
            return PartialView(users);
        }

        private ChatRoom GetChatRoom(string email)
        {
            var recipient = _userService.GetUserOrDefault(email);
            if (recipient == null || email == CurrentUserEmail)
            {
                return null;
            }

            var messages = recipient.IncomingMessages.Where(m => m.FromId == CurrentUserEmail).ToList();
            messages.AddRange(recipient.OutcomingMessages.Where(m => m.ToId == CurrentUserEmail));
            messages = messages.OrderBy(m => m.Date).ToList();
            var result = new MessageViewModel[messages.Count];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new MessageViewModel
                {
                    Date = messages[i].Date,
                    IsForMe = messages[i].ToId == CurrentUserEmail,
                    Sender = messages[i].From,
                    Text = messages[i].Text
                };
            }

            return new ChatRoom
            {
                Messages = result,
                Recipient = recipient,
                CurrentUser = _userService.GetUser(CurrentUserEmail)
            };
        }
    }
}