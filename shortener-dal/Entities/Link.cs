using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace url_shortener_server.shortener_dal.Entities
{
    public class Link
    {
        [Key]
        [Column("link_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Column("source",TypeName = "text")]
        public string Source { get; set; }

        [Column("shortened")]
        [StringLength(35)]
        public string Shortened { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("created_at", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        
        [Column("views")]
        public int Views { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Links")]
        public virtual User? User { get; set; }
    }
}
