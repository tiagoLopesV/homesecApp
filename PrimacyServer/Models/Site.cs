using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("site")]
public class Site{
    [Key]
    public int id { get; set; }
    public required string name { get; set; }
    public required int divisionId { get; set; }
    [ForeignKey("divisionId")]
    public Division? Division { get; set; }
}