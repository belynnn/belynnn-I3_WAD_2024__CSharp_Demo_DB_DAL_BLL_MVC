using System.ComponentModel;

namespace ASP_MVC.Models.Comment
{
	public class CommentDelete
	{
		[DisplayName("Commentaire : ")]
		public string Title { get; set; }

		[DisplayName("Contenu : ")]
		public string? Content { get; set; }

		[DisplayName("Créé par : ")]
		public Guid? CreatedBy { get; set; }
	}
}