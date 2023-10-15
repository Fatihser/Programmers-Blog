using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NToastNotify;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Mvc.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Helpers.Abstract;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OptionsController : Controller
    {
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsPageInfoWriter;
        private readonly IToastNotification _toastNotification;
        private readonly WebsiteInfo _webSiteInfo;
        private readonly IWritableOptions<WebsiteInfo> _websiteInfoWriter;
        private readonly SmtpSettings _smtpSettings;
        private readonly IWritableOptions<SmtpSettings> _smtpSettingsWriter;
        private readonly ArticleRightSideBarWidgetOptions _articleRightSideBarWidgetOptions;
        private readonly IWritableOptions<ArticleRightSideBarWidgetOptions> _articleRightSideBarWidgetOptionsWriter;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public OptionsController(IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo,
            IWritableOptions<AboutUsPageInfo> aboutUsPageInfoWriter, IToastNotification toastNotification,
            IOptionsSnapshot<WebsiteInfo> webSiteInfo, IWritableOptions<WebsiteInfo> wesiteInfoWriter,
            IOptionsSnapshot<SmtpSettings> smtpSettings, IWritableOptions<SmtpSettings> smtpSettingsWriter,
            IOptionsSnapshot<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptions,
            IWritableOptions<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptionsWriter, ICategoryService categoryService, IMapper mapper)
        {
            _aboutUsPageInfo = aboutUsPageInfo.Value;
            _aboutUsPageInfoWriter = aboutUsPageInfoWriter;
            _toastNotification = toastNotification;
            _webSiteInfo = webSiteInfo.Value;
            _websiteInfoWriter = wesiteInfoWriter;
            _smtpSettings = smtpSettings.Value;
            _smtpSettingsWriter = smtpSettingsWriter;
            _articleRightSideBarWidgetOptions = articleRightSideBarWidgetOptions.Value;
            _articleRightSideBarWidgetOptionsWriter = articleRightSideBarWidgetOptionsWriter;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }

        [HttpPost]
        public IActionResult About(AboutUsPageInfo aboutUsPageInfo)
        {
            if (ModelState.IsValid)
            {
                _aboutUsPageInfoWriter.Update(x =>
                {
                    x.Header = aboutUsPageInfo.Header;
                    x.Content = aboutUsPageInfo.Content;
                    x.SeoAuthor = aboutUsPageInfo.SeoAuthor;
                    x.SeoDescription = aboutUsPageInfo.SeoDescription;
                    x.SeoTags = aboutUsPageInfo.SeoTags;
                });
                _toastNotification.AddSuccessToastMessage("Hakkimizda Sayfa Icerikleriyle basariyla " +
                    "guncellenmistir.", new ToastrOptions
                    {
                        Title = "Basarili Islem!"
                    });
                return View(aboutUsPageInfo);
            }
            return View(aboutUsPageInfo);
        }

        [HttpGet]
        public IActionResult GeneralSettings()
        {
            return View(_webSiteInfo);
        }

        [HttpPost]
        public IActionResult GeneralSettings(WebsiteInfo websiteInfo)
        {
            if (ModelState.IsValid)
            {
                _websiteInfoWriter.Update(x =>
                {
                    x.Title = websiteInfo.Title;
                    x.MenuTitle = websiteInfo.MenuTitle;
                    x.SeoAuthor = websiteInfo.SeoAuthor;
                    x.SeoDescription = websiteInfo.SeoDescription;
                    x.SeoTags = websiteInfo.SeoTags;
                });
                _toastNotification.AddSuccessToastMessage(
                    "Sitenizin genel ayarlari basariyla " +
                    "guncellenmistir.", new ToastrOptions
                    {
                        Title = "Basarili Islem!"
                    });
                return View(websiteInfo);
            }
            return View(websiteInfo);
        }

        [HttpGet]
        public IActionResult EmailSettings()
        {
            return View(_smtpSettings);
        }

        [HttpPost]
        public IActionResult EmailSettings(SmtpSettings smtpSettings)
        {
            if (ModelState.IsValid)
            {
                _smtpSettingsWriter.Update(x =>
                {
                    x.Server = smtpSettings.Server;
                    x.Port = smtpSettings.Port;
                    x.SenderName = smtpSettings.SenderName;
                    x.SenderEmail = smtpSettings.SenderEmail;
                    x.Username = smtpSettings.Username;
                    x.Password=smtpSettings.Password;
                });
                _toastNotification.AddSuccessToastMessage(
                    "Sitenizin e-posta ayarlari basariyla " +
                    "guncellenmistir.", new ToastrOptions
                    {
                        Title = "Basarili Islem!"
                    });
                return View(smtpSettings);
            }
            return View(smtpSettings);
        }

        [HttpGet]
        public async Task<IActionResult> ArticleRightSideBarWidgetSettings()
        {
            var categoryResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            var articleRightSideBarWidgetOptionsViewModel = _mapper.Map<ArticleRightSideBarWidgetOptionsViewModel>(_articleRightSideBarWidgetOptions);
            articleRightSideBarWidgetOptionsViewModel.Categories = categoryResult.Data.Categories;
            return View(articleRightSideBarWidgetOptionsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ArticleRightSideBarWidgetSettings(ArticleRightSideBarWidgetOptionsViewModel articleRightSideBarWidgetOptions)
        {
            var categoryResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            articleRightSideBarWidgetOptions.Categories = categoryResult.Data.Categories;
            if (ModelState.IsValid)
            {
                _articleRightSideBarWidgetOptionsWriter.Update(x =>
                {
                    x.Header = articleRightSideBarWidgetOptions.Header;
                    x.TakeSize = articleRightSideBarWidgetOptions.TakeSize;
                    x.CategoryId = articleRightSideBarWidgetOptions.CategoryId;
                    x.FilterBy = articleRightSideBarWidgetOptions.FilterBy;
                    x.OrderBy = articleRightSideBarWidgetOptions.OrderBy;
                    x.IsAscending = articleRightSideBarWidgetOptions.IsAscending;
                    x.StartAt = articleRightSideBarWidgetOptions.StartAt;
                    x.EndAt = articleRightSideBarWidgetOptions.EndAt;
                    x.MaxViewCount = articleRightSideBarWidgetOptions.MaxViewCount;
                    x.MinViewCount = articleRightSideBarWidgetOptions.MinViewCount;
                    x.MaxCommentCount = articleRightSideBarWidgetOptions.MaxCommentCount;
                    x.MinCommentCount = articleRightSideBarWidgetOptions.MinCommentCount;
                });
                _toastNotification.AddSuccessToastMessage(
                    "Makale sayfalarinizin widget ayarlari basariyla " +
                    "guncellenmistir.", new ToastrOptions
                    {
                        Title = "Basarili Islem!"
                    });
                return View(articleRightSideBarWidgetOptions);
            }
            return View(articleRightSideBarWidgetOptions);
        }


    }
}
