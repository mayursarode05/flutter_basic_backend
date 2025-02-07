using Application.Repository.IRepository;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user =  await _unitOfWork.userRepository.FindAsync(u => u.UserName == loginDTO.UserName && u.Password == loginDTO.Password);
            if (user == null || user.Count() == 0) return Ok(new { message = "Invalid Credentials", success = false });
            return Ok(new { message = "Login successful", user ,success = true});
        }
      
    }
}
