public class Factura
{
    public long Id { get; set; }
    public DateTime Fecha { get; set; }
    public Cliente Cliente { get; set; }
    public List<Vehiculo> Vehiculos { get; set; }
    public decimal Total { get; set; }

    public Factura(long id, DateTime fecha, Cliente cliente, List<Vehiculo> vehiculos, decimal total)
    {
        Id = id;
        Fecha = fecha;
        Cliente = cliente;
        Vehiculos = vehiculos;
        Total = total;
    }
}
