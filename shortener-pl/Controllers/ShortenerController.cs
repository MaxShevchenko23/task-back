using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using url_shortener_server.Helpers;
using url_shortener_server.shortener_dal.Entities;

namespace url_shortener_server.Controllers
{
    [Route("shortener")]
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly LinkRepository repository;

        public ShortenerController(LinkRepository repository)
        {
            this.repository = repository;
        }

        [AllowAnonymous]
        [HttpPost("shorten")]
        public async Task<ActionResult<object>> ShortenLink(string source)
        {
            if (!source.IsValid(ToValidate.Link))
            {
                return BadRequest();
            }

            var shortened = await repository.GetShortenedLink(source);

            if (shortened != null)
            {
                return Ok(new 
                {
                    shortened = $"{Request.Scheme}://{Request.Host.Value}/shortener/{shortened}"
                });
            }
            else
            {
                var created = await repository
                    .CreateShortenedLink(source, JWTTranslator.GetUserId(HttpContext));

                return StatusCode(201, new
                {
                    shortened = $"{Request.Scheme}://{Request.Host.Value}/shortener/{created}"
                });
            }


        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Link>>> GetAllLinks()
        {
            Console.WriteLine(Request.Headers.Authorization);
            var userId = JWTTranslator.GetUserId(HttpContext);
            
            return Ok(await repository.GetAllLinks(userId));
        }

        [HttpGet("{shortened}")]
        public async Task<ActionResult<string>> RedirectToTheSource([FromRoute] string shortened)
        {
            var source = await repository.GetSourceLink(shortened);
            if (source != null)
            {
                return Redirect(source);
            }

            return NotFound();
        }

        [HttpGet("{shortened}/info")]
        public async Task<ActionResult<string>> GetInfoByCode([FromRoute] string shortened)
        {
            var source = await repository.GetInfo(shortened);
            if (source != null)
            {
                return Ok(source);
            }

            return NotFound();
        }

    }
}
