using Application.Mapper;
using Application.Repository.IRepository;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork, IMapper autoMapper)
        {
            this.unitOfWork = unitOfWork;       
            this._mapper = autoMapper;  
        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<IActionResult> AddNewUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await unitOfWork.userRepository.AddAsync(user);
            await unitOfWork.CommitAsync();
            return Ok("User Created successful..");
        }
    }
}
