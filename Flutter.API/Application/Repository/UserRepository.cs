using Application.Repository.IRepository;
using AutoMapper;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
