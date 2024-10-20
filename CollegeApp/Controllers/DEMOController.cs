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

        
        private readonly IMyLogger _myLogger2;

        public DEMOController(IMyLogger myLogger)
        {
            _myLogger2 = myLogger;
        }

        [HttpGet]
        public ActionResult Index2()
        {
            _myLogger2.Log("Index method stated");
            return Ok();
        }

        // Sonra Program.cs


        /// </summary>




    }
}
