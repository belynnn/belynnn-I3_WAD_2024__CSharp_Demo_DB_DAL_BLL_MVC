using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ASP_MVC.Handlers.ActionFilters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class IsCommentatorAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? json = context.HttpContext.Session.GetString(nameof(SessionManager.ConnectedUser));
            if (json is null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }
            ConnectedUser connectedUser = JsonSerializer.Deserialize<ConnectedUser>(json);
            Guid comment_id = Guid.Parse(context.RouteData.Values["id"].ToString());
			ICommentRepository<Comment> commentRepository = GetCommentService(context.HttpContext);
			Comment comment = commentRepository.Get(comment_id);
            if(comment.CreatedBy != connectedUser.User_Id)
            {
                context.Result = new RedirectToActionResult("Details", "Comment", new { id = comment_id });
            }
        }

        private ICommentRepository<Comment> GetCommentService(HttpContext httpContext)
        {
            IServiceProvider serviceProvider = httpContext.RequestServices;
            return serviceProvider.GetService<ICommentRepository<Comment>>();
        }
    }
}