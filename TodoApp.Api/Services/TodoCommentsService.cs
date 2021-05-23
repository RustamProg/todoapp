using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Api.DTOs;
using TodoApp.Api.Models.DbContexts;
using TodoApp.Api.Models.DbEntities;

namespace TodoApp.Api.Services
{
    public class TodoCommentsService: ITodoCommentsService
    {
        // todo: add user 
        private readonly IDbRepository _dbRepository;
        private readonly SqlServerDbContext _dbContext;

        public TodoCommentsService(IDbRepository dbRepository, SqlServerDbContext dbContext)
        {
            _dbRepository = dbRepository;
            _dbContext = dbContext;
        }

        public TodoComment GetCommentById(long id)
        {
            return _dbRepository.GetById<TodoComment>(id);
        }

        public List<TodoComment> GetCommentsByTodo(long todoId)
        {
            return _dbContext.TodoComments.Where(x => x.ParentTodo.Id == todoId).ToList();
        }

        public List<TodoComment> GetAllComments()
        {
            return _dbRepository.GetAll<TodoComment>().ToList();
        }

        public async Task PostComment(TodoCommentDto todoCommentDto)
        {
            var comment = new TodoComment
            {
                AuthorName = "Not impl",
                AuthorId = Guid.NewGuid(),
                CommentBody = todoCommentDto.CommentBody,
                ParentTodoId = todoCommentDto.ParentTodoId,
            };

            await _dbRepository.AddAsync(comment);
        }
    }
}