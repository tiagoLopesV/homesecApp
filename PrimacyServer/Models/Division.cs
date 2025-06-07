using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("division")] 
public class Division
{
    [Key]
    public int id { get; set; }
    public required string name { get; set; }
    public required int entityId { get; set; }
    
    [ForeignKey("entityId")]
    [JsonIgnore]
    public Entity? Entity { get; set; }

    public ICollection<Site> Sites { get; set; } = new List<Site>();
}
