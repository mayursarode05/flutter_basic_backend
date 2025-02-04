using Application.Repository.IRepository;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository UserRepository { get; private set; }
        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
            this.UserRepository = new UserRepository(this._context);
        }

        //public IUserRepository userRepository => throw new NotImplementedException();
        public IUserRepository userRepository
        {
            get
            {
                return UserRepository ??= new UserRepository(_context);
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
