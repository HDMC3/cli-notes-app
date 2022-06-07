using Postgrest.Attributes;
using Postgrest.Models;

namespace Services.Models {
    [Table("notebooks")]
    public class Notebook : BaseModel {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }
        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;
    }
}