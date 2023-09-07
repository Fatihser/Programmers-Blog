using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Services.Abstract;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;

        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId,int currentPage=1,int pageSize=5,bool isAscending=false)
        {
            var articlesResult = await (categoryId == null 
                ? _articleService.GetAllByPagingAsync(null,currentPage,pageSize, isAscending) :
                _articleService.GetAllByPagingAsync(categoryId.Value,currentPage,pageSize, isAscending));
            return View(articlesResult.Data);
        }
    }
}
