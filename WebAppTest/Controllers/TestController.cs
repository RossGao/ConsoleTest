using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTest.Controllers
{
    [Route("test")]
    public class TestController : Controller
    {
        [Route("employee")]
        [HttpGet]
        public IActionResult GetEmployee(int age)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Successfully get employee");
                return Ok();
            }

            Console.WriteLine("Invalid request parameter value");
            return BadRequest();
        }
    }
}
