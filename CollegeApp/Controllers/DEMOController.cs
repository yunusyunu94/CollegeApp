using Dependency_Injection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CtyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DEMOController : ControllerBase
    {
        //1. Strong coupled / tightly coupled

        private readonly IMyLogger _myLogger;

        public DEMOController()
        {
            _myLogger = new LogToFile();
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("Index method stated");
            return Ok();
        }






        //2. Loosely coupled
    }
}
