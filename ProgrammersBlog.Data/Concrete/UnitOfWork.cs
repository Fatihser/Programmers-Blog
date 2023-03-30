using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProgrammersBlogContext _context;
        private EFArticleRepository _articleRepository;
        private EFCategoryRepository _categoryRepository;
        private EFCommentRepository _commentRepository;
        private EFRoleRepository _roleRepository;
        private EFUserRepository _userRepository;
        public UnitOfWork(ProgrammersBlogContext context)
        {
            _context = context;
        }

        public IArticleRepository Articles => _articleRepository??new EFArticleRepository(_context);

        public ICateogryRepository Categories => _categoryRepository ?? new EFCategoryRepository(_context);

        public ICommentRepository Comments => _commentRepository ?? new EFCommentRepository(_context);

        public IRoleRepository Roles => _roleRepository ?? new EFRoleRepository (_context);

        public IUserRepository Users => _userRepository ?? new EFUserRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
