using System.ComponentModel.DataAnnotations;

namespace url_shortener_server.Helpers.Models
{
    public class AuthModel : IValidatableObject
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (!Email.IsValid(ToValidate.Email))
            {
                errors.Add(new("Email is invalid"));
            }

            if (!Password.IsValid(ToValidate.Password))
            {
                errors.Add(new("Password is invalid"));
            }

            return errors;
        }
    }
}
