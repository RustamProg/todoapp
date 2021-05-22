using System;

namespace TodoApp.Api.Services
{
    public interface ICurrentUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}