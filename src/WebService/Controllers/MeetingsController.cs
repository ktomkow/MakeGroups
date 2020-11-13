﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using WebService.Commands;
using WebService.Services;

namespace WebService.Controllers
{
    public class MeetingsController : Controller
    {
        private readonly IMeetingService meetingService;

        public MeetingsController(IMeetingService meetingService)
        {
            this.meetingService = meetingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var meetings = this.meetingService.Get();

            return View(meetings);
        }

        [HttpGet]
        public IActionResult Participants(Guid meetingId)
        {
            var viewModel = this.meetingService.Participants(meetingId);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult MeetingCreation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string meetingName)
        {
            this.meetingService.Create(meetingName);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Join(Guid meetingId)
        {
            string username = this.User.Identity.Name;
            this.meetingService.JoinAsMember(meetingId, username);

            return RedirectToAction("Participants", new { meetingId = meetingId});
        }

        [HttpPost]
        public IActionResult JoinAsMember(JoinMeeting command)
        {
            this.meetingService.JoinAsMember(command.MeetingId, command.UserId);

            return RedirectToAction("Participants", new { meetingId = command.MeetingId });
        }

        [HttpPost]
        public IActionResult JoinAsLeader(JoinMeeting command)
        {
            this.meetingService.JoinAsLeader(command.MeetingId, command.UserId);

            return RedirectToAction("Participants", new { meetingId = command.MeetingId });
        }

        [HttpPost]
        public IActionResult Leave(Guid meetingId)
        {
            string username = this.User.Identity.Name;
            this.meetingService.Leave(meetingId, username);

            return RedirectToAction("Index");
        }
    }
}
