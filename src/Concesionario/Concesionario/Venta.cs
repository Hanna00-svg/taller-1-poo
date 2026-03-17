public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public Cliente Cliente { get; set; }
    public List<Vehiculo> Vehiculos { get; set; }
    public Factura Factura { get; set; }

    public void GenerarFactura()
    {
        decimal total = Vehiculos.Sum(v => v.CalcularPrecioFinal());
        foreach (var v in Vehiculos) v.Vendido = true;
        Factura = new Factura(Id, Fecha, Cliente, Vehiculos, total);
    }
}
