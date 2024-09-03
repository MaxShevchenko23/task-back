using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using url_shortener_server.Helpers;

namespace url_shortener_server.shortener_dal.Entities
{
    public class User : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int Id { get; set; }

        [Column("password")]
        [StringLength(20)]
        public string Password { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("user_role_id")]
        public int UserRoleId { get; set; }

        [ForeignKey(nameof(UserRoleId))]
        [InverseProperty("Users")]
        public virtual UserRole? UserRole { get; set; }

        [InverseProperty(nameof(Link.User))]
        public virtual ICollection<Link> Links { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new();

            if (!Email.IsValid(ToValidate.Email))
            {
                errors.Add(new("This is not correct email!"));
            }

            if (!Password.IsValid(ToValidate.Password))
            {
                errors.Add(new("This is not valid password!"));
            }

            return errors;
        }
    }
}
