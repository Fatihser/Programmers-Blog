﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NToastNotify;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Helpers.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IMailService _mailService;
        private readonly IToastNotification _toastNotification;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsPageInfoWriter;

        public HomeController(IArticleService articleService, IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo, IMailService mailService, IToastNotification toastNotification, IWritableOptions<AboutUsPageInfo> aboutUsPageInfoWriter)
        {
            _articleService = articleService;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
            _mailService = mailService;
            _toastNotification = toastNotification;
            _aboutUsPageInfoWriter = aboutUsPageInfoWriter;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId,int currentPage=1,int pageSize=5,bool isAscending=false)
        {
            var articlesResult = await (categoryId == null 
                ? _articleService.GetAllByPagingAsync(null,currentPage,pageSize, isAscending) :
                _articleService.GetAllByPagingAsync(categoryId.Value,currentPage,pageSize, isAscending));
            return View(articlesResult.Data);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(EmailSendDto emailSendDto)
        {
            if (ModelState.IsValid)
            {
                var result = _mailService.SendContactEmail(emailSendDto);
                _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                {
                    Title = "Basarili islem!"
                });
                return View();
            }
            else
            {
                _toastNotification.AddErrorToastMessage("bi hata var", new ToastrOptions
                {
                    Title = "Basarili islem!"
                });
                return View();
            }
        }
    }
}
