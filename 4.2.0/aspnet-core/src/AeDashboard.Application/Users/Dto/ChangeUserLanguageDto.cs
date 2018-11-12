using System.ComponentModel.DataAnnotations;

namespace AeDashboard.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}