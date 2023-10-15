using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProgrammersBlog.Shared.Entities.Concrete;
using System;
using System.Data.SqlTypes;

namespace ProgrammersBlog.Mvc.Filters
{
    public class MvcExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _enviroment;
        private readonly IModelMetadataProvider _metadataProvider;
        private readonly ILogger _logger;

        public MvcExceptionFilter(IHostEnvironment enviroment, IModelMetadataProvider metadataProvider, ILogger<MvcExceptionFilter> logger)
        {
            _enviroment = enviroment;
            _metadataProvider = metadataProvider;
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            if (_enviroment.IsDevelopment())
            {
                context.ExceptionHandled=true;
                var mvcErrorModel = new MvcErrorModel();
                ViewResult result;
                switch (context.Exception)
                {
                    case SqlNullValueException:
                        mvcErrorModel.Message =
                            $"Uzgunuz isleminiz sirasinda beklenmedik bir veritabani hatasi olustu.Sorunu kisa surede cozecegiz.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 500;
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;
                    case NullReferenceException:
                        mvcErrorModel.Message =
                            $"Uzgunuz isleminiz sirasinda beklenmedik bir null veriye rastlandi.Sorunu kisa surede cozecegiz.";
                        mvcErrorModel.Detail = context.Exception.Message;
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 403;
                        _logger.LogError(context.Exception, context.Exception.Message);
                        break;
                    default:
                        mvcErrorModel.Message =
                            $"Uzgunuz isleminiz sirasinda beklenmedik bir hata olustu.Sorunu kisa surede cozecegiz.";
                        result = new ViewResult { ViewName = "Error" };
                        result.StatusCode = 500;
                        _logger.LogError(context.Exception,"Bu benim log hata mesajim!");
                        break;

                }
                result.ViewData = new ViewDataDictionary(_metadataProvider, context.ModelState);
                result.ViewData.Add("MvcModelError", mvcErrorModel);
                context.Result = result;
            }
        }
    }
}
