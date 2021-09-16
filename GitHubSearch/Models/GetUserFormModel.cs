using System.ComponentModel.DataAnnotations;

namespace GitHubSearch.Models
{
    public class GetUserFormModel
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Username { get; set; }
    }
}