using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirebirdDemoApp.Vehicles.Domains.Entities;

public class Vehicle
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Column("name", TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Column("brand", TypeName = "varchar(100)")]
    public string Brand { get; set; } = string.Empty;
    
    [Required]
    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }
    
    [Required]
    [Column("release_year", TypeName = "int")]
    public int ReleaseYear { get; set; }
}