using System;

namespace TodoApp.Api.Services.Utils
{
    public class CurrentUser : ICurrentUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}