public class Factura
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public Cliente Cliente { get; set; }
    public List<Vehiculo> Vehiculos { get; set; }
    public decimal Total { get; set; }

    public Factura() { }

    public Factura(int id, DateTime fecha, Cliente cliente, List<Vehiculo> vehiculos, decimal total)
    {
        Id = id;
        Fecha = fecha;
        Cliente = cliente;
        Vehiculos = vehiculos;
        Total = total;
    }

    public override string ToString()
    {
        return $"Factura #{Id} - Fecha: {Fecha:d} - Cliente: {Cliente.Nombre} - Total: {Total:C}";
    }
}
