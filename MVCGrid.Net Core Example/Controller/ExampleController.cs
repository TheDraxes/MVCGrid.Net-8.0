using Microsoft.AspNetCore.Mvc;
using MVCGrid.Net_Core_Example.Models;
using MVCGrid.NetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MVCGrid.Net_Core_Example
{
    public class ExampleController : Controller
    {
        public IActionResult Basic()
        {
            return View();
        }
        public IActionResult SignalRExample()
        {
            return View();
        }
        public async Task<IActionResult> SignalRTest()
        {
            Task task = Task.Run(SignalRTestJob);
            return Content(string.Empty);
        }
        public async Task SignalRTestJob()
        {
            for (int x = 0; 500 > x; x++)
            {
                int count = MVCGridSignalR.SignalRGridSessions["TestGrid2"].Data.Count();
                Person person = new Person()
                {
                    Id = count,
                    FirstName = "Alpha",
                    LastName = "Shabazz",
                    Active = true,
                    Email = "test@gmail.com",
                    Employee = true,
                    Gender = "Male",
                    StartDate = DateTime.Now,
                };
                MVCGridSignalR.SignalRGridSessions["TestGrid2"].Data.Add(person);
                await Task.Delay(100);
            }
        }

    }
}