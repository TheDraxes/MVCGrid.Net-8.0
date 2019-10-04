using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVCGrid.Net_Core_Example.Models;
using MVCGrid.NetCore.SignalR;
using MVCGrid.NetCore.SignalR.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCGrid.Net_Core_Example
{
    public class ExampleController : Controller
    {
        public IActionResult BasicExample()
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
            for (int x=0; 500 > x; x++)
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