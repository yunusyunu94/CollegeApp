using Dependency_Injection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CtyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DEMOController : ControllerBase
    {
        /// <summary>
        
        ///  ------------------------------------- Dependency Injection ; ---------------------------------------------------
          
         
        ///  1. Strong coupled / tightly coupled

        //private readonly IMyLogger _myLogger;

        //public DEMOController()
        //{
        //    _myLogger = new LogToFile();
        //}

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    _myLogger.Log("Index method stated");
        //    return Ok();
        //}




        // 2. Loosely coupled

        
        private readonly ILogger<DEMOController> _logger;

        public DEMOController(ILogger<DEMOController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index2()
        {
            _logger.LogTrace("Log Message from Trace method");
            _logger.LogDebug("Log Message from Debug method");
            _logger.LogInformation("Log Message from Information method");
            _logger.LogWarning("Log Message from Warning method");
            _logger.LogError("Log Message from Error method");
            _logger.LogCritical("Log Message from Critical method");
            return Ok();
        }

        // Sonra Program.cs


        /// </summary>




    }
}
