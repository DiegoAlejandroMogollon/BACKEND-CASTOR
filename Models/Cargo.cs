
using System.ComponentModel.DataAnnotations;
namespace CASTOR_API.Models
{
public class Cargo
{
    [Key]
    public int IdCargo { get; set; }
    public string Nombre { get; set; }
}
}