using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Crew.Api.ReferenceImpl.V1.Controllers
{
    [ApiController]
    [ResponseCache(CacheProfileName = "No")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("version")]
        public string Version() => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
    }
}