using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using TodoApp.Api.Services.Utils;

namespace TodoApp.Api.Helpers
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context, ICurrentUser currentUser)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            if (jsonToken != null)
            {
                currentUser.Id = new Guid(jsonToken.Claims.First(x => x.Type == "sub").Value);
                currentUser.Username = jsonToken.Claims.First(x => x.Type == "Username").Value;
                currentUser.Email = jsonToken.Claims.First(x => x.Type == "Email").Value;
            }
            
            await _next(context);
        }
    }
}