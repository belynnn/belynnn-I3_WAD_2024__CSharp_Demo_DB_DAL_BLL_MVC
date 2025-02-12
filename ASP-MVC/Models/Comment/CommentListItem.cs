using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models.Comment
{
	public class CommentListItem
	{
		[ScaffoldColumn(false)]
		public Guid Comment_Id { get; set; }

		[DisplayName("Titre : ")]
		public string Title { get; set; }

		[DisplayName("Commentaire : ")]
		public string? Content { get; set; }

		[DisplayName("Note : ")]
		public int? Note { get; set; }
	}
}