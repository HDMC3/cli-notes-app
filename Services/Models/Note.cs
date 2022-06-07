using Postgrest.Attributes;
using Postgrest.Models;

namespace Services.Models {
    [Table("notes")]
    public class Note : BaseModel {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }
        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [Column("title")]
        public string Title { get; set; } = null!;
        [Column("description")]
        public string Description { get; set; } = null!;
        [Column("notebook_id")]
        public Guid NotebookId { get; set; }
    }
}