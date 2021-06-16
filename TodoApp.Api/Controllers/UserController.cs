using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Services.Utils;

namespace TodoApp.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        
        /// <summary>
        /// Контроллер управления пользователем
        /// </summary>
        /// <param name="currentUser"></param>
        public UserController(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }
        
        /// <summary>
        /// Получить информацию о текущем пользователе
        /// </summary>
        /// <returns></returns>
        [HttpGet("current-user")]
        public ICurrentUser GetCurrentUser()
        {
            return _currentUser;
        }
    }
}