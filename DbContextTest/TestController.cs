using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DbContextTest
{
    [Route("Test")]
    public class TestController : Controller
    {
        private ITestRepository repository;

        public TestController(ITestRepository theRepo)
        {
            repository = theRepo;
        }

        [Route("multiThreading")]
        [HttpGet]
        public IActionResult TestMultiThreading()
        {
            var IdList = new List<string>();

            Parallel.For(0, 10, i =>
            {
                IdList.Add($"{repository.DbContext.Id}. The current thread Id is: {Thread.CurrentThread.ManagedThreadId}.");
                //Console.WriteLine($"{repository.DbContext.Id}. The current thread Id is: {Thread.CurrentThread.ManagedThreadId}.");
            });

            return Ok(IdList);
        }

        [HttpPost]
        public IActionResult AddTest(Test added)
        {
            if (ModelState.IsValid && added != null)
            {
                if (repository.AddTest(added))
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateTest(Test update)
        {
            if (ModelState.IsValid && update != null)
            {
                if (repository.UpdateTest(update))
                {
                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }

        [Route("name")]
        [HttpPost]
        public IActionResult ChangeName(Test changed)
        {
            if (changed != null)
            {
                if (repository.ChangeName(changed))
                {
                    return Ok();
                }

                return NotFound();
            }

            return BadRequest();
        }
    }
}
