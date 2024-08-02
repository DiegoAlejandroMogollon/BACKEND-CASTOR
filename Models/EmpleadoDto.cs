public class EmpleadoDto
{
    public int Id { get; set; }
    public int Cedula { get; set; }
    public string Nombre { get; set; }
    public string FotoRuta { get; set; }
    public DateTime FechaIngreso { get; set; }
    public CargoDto Cargo { get; set; }
}