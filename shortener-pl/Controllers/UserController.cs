using Microsoft.AspNetCore.Mvc;
using url_shortener_server.Helpers;
using url_shortener_server.Helpers.Models;
using url_shortener_server.shortener_dal.Repositories;

namespace url_shortener_server.shortener_pl.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository repository;
        private readonly IConfiguration configuration;

        public UserController(UserRepository repository,
            IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(AuthModel model)
        {
            var entity = await repository.CreateUser(model);
            
            if (entity == null)
            {
                return StatusCode(500);
            }

            string token = JWTConstructor.Build(entity, configuration);

            //Response.Cookies.Append("token", token);

            return StatusCode(201, new { token });

        }


        [HttpPost("auth")]
        public async Task<ActionResult> Authenticate(AuthModel model)
        {
            var entity = await repository.Authenticate(model.Email, model.Password);

            if (entity == null)
            {
                return NotFound();
            }

            string token = JWTConstructor.Build(entity, configuration);

            //Response.Cookies.Append("token", token);

            return Ok(new { token });

        }
    }
}
