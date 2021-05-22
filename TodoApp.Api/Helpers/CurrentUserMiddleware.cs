using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using TodoApp.Api.Services;

namespace TodoApp.Api.Helpers
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context, ICurrentUser currentUser, HttpClient httpClient)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var discoveryResponse = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = "https://localhost:5001",
                Policy =
                {
                    ValidateIssuerName = false
                }
            });
            

            var userInfo = await httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Address = discoveryResponse.UserInfoEndpoint,
                Token = token
            });

            if (!userInfo.IsError)
            {
                currentUser.Username = userInfo.Claims.FirstOrDefault(x => x.Type == "name").Value;
                currentUser.Id = new Guid(userInfo.Claims.FirstOrDefault(x => x.Type == "sub").Value);
                currentUser.Email = userInfo.Claims.FirstOrDefault(x => x.Type == "email").Value;
            }
            await _next(context);
        }
    }
}