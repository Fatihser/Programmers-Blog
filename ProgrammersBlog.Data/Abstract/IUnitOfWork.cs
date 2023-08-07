using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IArticleRepository Articles { get; } //unitofwork.Articles
        ICateogryRepository Categories { get; }
        ICommentRepository Comments { get; }
        //_unitofwork.categories.addasync(category);
        //_unitofwork.users.addasync(user);
        //_unitofwork.saveasync();
        Task<int> SaveAsync(); 
    }
}
