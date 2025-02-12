using ASP_MVC.Handlers;
using ASP_MVC.Handlers.ActionFilters;
using ASP_MVC.Mappers;
using ASP_MVC.Models.Cocktail;
using ASP_MVC.Models.Comment;
using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
	public class CommentController : Controller
	{
		private ICommentRepository<Comment> _commentRepository;
		private SessionManager _sessionManager;

		public CommentController(
			ICommentRepository<Comment> commentRepository,
			SessionManager sessionManager
			)
		{
			_commentRepository = commentRepository;
			_sessionManager = sessionManager;
		}
		// GET: CommentController
		public ActionResult Index()
		{
			try
			{
				ConnectedUser? user = _sessionManager.ConnectedUser;
				if (user == null) throw new NullReferenceException();
				Guid userId = user.User_Id;
				IEnumerable<CommentListItem> model = _commentRepository.GetFromUser(userId).Select(bll => bll.ToListItem());
				return View(model);
			}
			catch (Exception)
			{
				return RedirectToAction("Error", "Home");
			}
		}

		// GET: CommentController/Details/5
		public ActionResult Details(Guid id)
		{
			try
			{
				CommentDetails model = _commentRepository.Get(id).ToDetails();
				_sessionManager.AddVisitedCocktail(model.Comment_Id, model.Title);
				return View(model);
			}
			catch (Exception)
			{
				return RedirectToAction("Error", "Home");
			}
		}

		// GET: CommentController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CommentController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CommentCreateForm form)
		{
			try
			{
				if (!ModelState.IsValid) throw new ArgumentException(nameof(form));
				Guid id = _commentRepository.Insert(form.ToBLL());
				return RedirectToAction(nameof(Details), new { id });
			}
			catch
			{
				return View();
			}
		}

		// GET: CommentController/Edit/5
		public ActionResult Edit(Guid id)
		{
			try
			{
				CommentEditForm model = _commentRepository.Get(id).ToEditForm();
				return View(model);
			}
			catch (Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction(nameof(Index));
			}
		}

		// POST: CommentController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Guid id, CommentEditForm form)
		{
			try
			{
				if (!ModelState.IsValid) throw new ArgumentException(nameof(form));
				_commentRepository.Update(id, form.ToBLL());
				return RedirectToAction(nameof(Details), new { id });
			}
			catch
			{
				return View();
			}
		}

		// GET: CommentController/Delete/5
		//[ConnectionNeeded]
		[IsCommentator]
		public ActionResult Delete(Guid id)
		{
			try
			{
				CommentDelete model = _commentRepository.Get(id).ToDelete();
				return View(model);
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(Index));
			}
		}

		// POST: CommentController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		//[ConnectionNeeded]
		[IsCommentator]
		public ActionResult Delete(Guid id, CommentDelete form)
		{
			try
			{
				_commentRepository.Delete(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}