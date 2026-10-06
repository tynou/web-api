using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;
using WebApi.MinimalApi.Domain;
using WebApi.MinimalApi.Models;

namespace WebApi.MinimalApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller
{
    private readonly IUserRepository userRepository;
    private readonly IMapper autoMapper;
    
    // Чтобы ASP.NET положил что-то в userRepository требуется конфигурация
    public UsersController(IUserRepository repo, IMapper mapper)
    {
        userRepository = repo;
        autoMapper = mapper;
    }

    [HttpGet("{userId}")]
    [Produces("application/json", "application/xml")]
    [SwaggerResponse(200, "OK", typeof(UserDto))]
    [SwaggerResponse(404, "Пользователь не найден")]
    public ActionResult<UserDto> GetUserById([FromRoute] Guid userId)
    {
        var user = userRepository.FindById(userId);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(autoMapper.Map<UserDto>(user));
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] object user)
    {
        throw new NotImplementedException();
    }
}