using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models.Comment
{
	public class CommentCreateForm
	{
		[DisplayName("Titre : ")]
		[Required(ErrorMessage = "Le champ 'Titre' est obligatoire.")]
		[MinLength(2, ErrorMessage = "Le champ 'Titre' doit contenir au minimum 2 caractères.")]
		[MaxLength(64, ErrorMessage = "Le champ 'Titre' doit contenir au maximum 64 caractères.")]
		public string Title { get; set; }

		[DisplayName("Commentaire : ")]
		[MinLength(2, ErrorMessage = "Le champ 'Commentaire' doit contenir au minimum 2 caractères.")]
		[MaxLength(512, ErrorMessage = "Le champ 'Commentaire' doit contenir au maximum 512 caractères.")]
		public string? Content { get; set; }

		[DisplayName("Note : ")]
		[Required(ErrorMessage = "Le champ 'Note' est obligatoire.")]
		public int? Note { get; set; }

		[HiddenInput]
		public Guid CreatedBy { get; set; }
	}
}