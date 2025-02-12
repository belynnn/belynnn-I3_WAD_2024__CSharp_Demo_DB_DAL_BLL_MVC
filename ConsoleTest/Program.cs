using BLL.Entities;
using BLL.Services;
using DAL.Entities;
using D = DAL.Services;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
			/*CocktailService service = new CocktailService();
            foreach (Cocktail c in service.Get())
            {
                Console.WriteLine($"{c.Cocktail_Id} : {c.Name}\nCréé le : {c.CreatedAt.ToShortDateString()}\n{c.Description}\n{c.Instructions}");
            }*/

			#region Test DAL Comment
			//CommentService service = new CommentService();
			//Comment comment1 = new Comment()
			//{
			//	Title = "Waw",
			//	Note = 5,
			//	Content = "Beaucoup trop bieng",
			//	CreatedBy = Guid.Parse("27f35ac8-6053-4649-95f7-21979e93451d"),
			//	Concern = Guid.Parse("f4b88396-b59e-4617-a462-0923aed690fb"),
			//	CreatedAt = DateTime.Now
			//};

			//comment1.Comment_Id = service.Insert(comment1);

			//foreach (Comment comment in service.GetFromUser(Guid.Parse("27f35ac8-6053-4649-95f7-21979e93451d")))
			//{
			//	Console.WriteLine($"{comment.Comment_Id} : {comment.Title}\nCréé le : {comment.CreatedAt.ToShortDateString()}\n{comment.Note}\n{comment.Concern}");
			//}
			#endregion

			#region Test BLL Comment
			//// Initialisation des services DAL
			//var commentDalService = new D.CommentService();
			//var cocktailDalService = new D.CocktailService();
			//var userDalService = new D.UserService();

			//// Instanciation de la BLL
			//var commentService = new BLL.Services.CommentService(commentDalService, cocktailDalService, userDalService);

			//Console.WriteLine("=== Test GetFromUser ===");
			//foreach (var comment in commentService.GetFromUser(Guid.Parse("27f35ac8-6053-4649-95f7-21979e93451d")))
			//{
			//	Console.WriteLine($"{comment.Comment_Id} : {comment.Title} - {comment.Note}/5");
			//}
			#endregion
		}
	}
}