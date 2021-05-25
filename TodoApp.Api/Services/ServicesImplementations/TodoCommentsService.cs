using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Api.DTOs;
using TodoApp.Api.Models.DbEntities;
using TodoApp.Api.Services.Repository;
using TodoApp.Api.Services.ServicesAbstractions;
using TodoApp.Api.Services.Utils;

namespace TodoApp.Api.Services.ServicesImplementations
{
    public class TodoCommentsService: ITodoCommentsService
    {
        // todo: add user 
        private readonly IDbRepository _dbRepository;
        private readonly ICurrentUser _currentUser;

        public TodoCommentsService(IDbRepository dbRepository, ICurrentUser currentUser)
        {
            _dbRepository = dbRepository;
            _currentUser = currentUser;
        }

        public TodoComment GetCommentById(long id)
        {
            return _dbRepository.GetById<TodoComment>(id);
        }

        public List<TodoComment> GetCommentsByTodo(long todoId)
        {
            return _dbRepository.Find<TodoComment>(x => x.ParentTodoId == todoId).ToList();
        }

        public List<TodoComment> GetAllComments()
        {
            return _dbRepository.GetAll<TodoComment>().ToList();
        }

        public async Task PostComment(TodoCommentDto todoCommentDto)
        {
            var comment = new TodoComment
            {
                AuthorName = _currentUser.Username,
                AuthorId = _currentUser.Id,
                CommentBody = todoCommentDto.CommentBody,
                ParentTodoId = todoCommentDto.ParentTodoId,
            };

            await _dbRepository.AddAsync(comment);
        }
    }
}