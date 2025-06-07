using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("entity")] 
public class Entity
{
    [Key]
    public int id { get; set; }
    public required string name { get; set; }

    public ICollection<Division> Divisions { get; set; } = new List<Division>();
}
