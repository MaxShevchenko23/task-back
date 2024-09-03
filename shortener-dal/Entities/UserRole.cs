using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace url_shortener_server.shortener_dal.Entities
{
    public class UserRole
    {
        [Key]
        [Column("user_role_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Column("user_role")]
        public string Role { get; set; }

        [InverseProperty(nameof(User.UserRole))]
        public virtual ICollection<User> Users { get; set; }

    }
}
