using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SomeTestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index2()
        {
            await Task.Delay(100);
            return Ok("HOmePage");
        }

        [HttpGet]
        [Route("string")]
        public IActionResult Index()
        {
            return Ok("Some random string");
        }

        [HttpGet]
        [Route("advanced/{text}")]
        public IActionResult Testing(string text)
        {
            return Problem("Ja jebie, jestes tępy");
        }

        [HttpGet]
        [Route("advancedNumber/{numberOfDupas}")]
        public IActionResult Testing(int numberOfDupas)
        {
            string result = string.Empty;

            for(int i = 0; i < numberOfDupas; i++)
            {
                result += "\r\nDupa";
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("postTest")]
        public IActionResult Posting([FromBody]PostTest value)
        {
            string result = string.Empty;

            for (int i = 0; i < value.repeat; i++)
            {
                result += $"\r\n{value.text}";
            }

            return Ok(result);
        }
    }

    public class PostTest
    {
        public string text { get; set; }
        public int repeat { get; set; }
    }
}
