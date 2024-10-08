using Microsoft.AspNetCore.Mvc;
using picpay_simplificado.Dtos;
using picpay_simplificado.Enums;
using picpay_simplificado.Models;
using picpay_simplificado.Repository;

namespace picpay_simplificado.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository repository)
        {
            _userRepository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateUserDto data)
        {
            if (data == null) return BadRequest("An user was not provided.");

            User newUser = new()
            {
                Name = data.Name,
                Email = data.Email,
                CPF = data.CPF,
                Type = data.Type,
                Password = data.Password
            };

            var user = await _userRepository.Create(newUser);

            return Ok(user);
        }
    }
}