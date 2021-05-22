using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("todo")]
    public class TodoController : Controller
    {
        [HttpGet("get_todos")]
        public List<string> GetTodos()
        {
            return new List<string>
            {
                "Learn ASP",
                "Learn IS4",
                "Learn something",
            };
        }
    }
}