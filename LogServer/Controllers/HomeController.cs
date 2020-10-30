using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LogServer.Models;
using System.IO;
using System.Text;
using System.Threading.Channels;
using Microsoft.AspNetCore.Http.Connections;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace LogServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly Class c;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, Class c)
        {
            _logger = logger;
            this.c = c;
        }


        public async Task<IActionResult> Index()
        {
            string bodyStr = "";
            using (StreamReader reader
                  = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync();
                if (!string.IsNullOrEmpty(bodyStr))
                {
                    await c.Datas.AddAsync(new Data { Content = bodyStr });
                    await c.SaveChangesAsync();
                }
            }
            return Json(true);
        }
        [Route("~/logs")]
        public async Task<ActionResult> Logs()
        {
            return View("Privacy");
        }

        public async Task<List<Data>> data()
        {
            
            return await c.Datas.ToListAsync();
        }


 
    }
}
