using Application.Mapper;
using Application.Repository.IRepository;
using AutoMapper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            if (userDto != null)
            {
                var existingUser =  await unitOfWork.userRepository.GetByEmailAsync(userDto.Email);
                if (existingUser == null) 
                {
                    var user = _mapper.Map<User>(userDto);
                    await unitOfWork.userRepository.AddAsync(user);
                    await unitOfWork.CommitAsync();
                    return Ok(new { message = "User Created successful..", success = true });
                }
                else
                {
                    return Ok(new { message = "User Already exists", success = false });
                }
            }
            else
            {
                return BadRequest();
            }
            
        }


        [Route("UpdateUserDetails")]
        [HttpPatch]
        public async Task<IActionResult> UpdateUserDetails(UserDto userDto)
        {
            var user = await unitOfWork.userRepository.GetByEmailAsync(userDto.Email);
            if (user == null)
            {
                return NotFound(new { message = "User not found.", success = false });
            }
            var Id = user.UserId;
            _mapper.Map(userDto, user);
            user.UserId = Id;
            if (user != null)
            {
                await unitOfWork.userRepository.UpdateAsync(user);
            }
            await unitOfWork.CommitAsync();
            return Ok(user);
        }

        [Route("GetUserInfo")]
        [HttpGet]
        public async Task<IActionResult> GetUserInfo(Guid userId)
        {
            try
            {
                User User =  await unitOfWork.userRepository.GetByIdAsync(userId);
                if (User == null)
                {
                    return NotFound(new { success = false, message = "User not found" });
                }
                return Ok(User);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
