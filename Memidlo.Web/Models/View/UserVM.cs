using Memidlo.Web.Models.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Memidlo.Web.Models.View
{
    public class UserVM
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$",
         ErrorMessage = "Haslo musi skladać się przynajmniej z ośmiu znaków oraz zawierać przynajmniej jedną cyfrę, " +
            "jedną wielką literę, jedną malą literę oraz znak specjalny")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        
    }
}
