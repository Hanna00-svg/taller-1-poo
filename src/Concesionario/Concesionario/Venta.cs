public class Venta : IPersistible
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public Cliente Cliente { get; set; }
    public List<Vehiculo> Vehiculos { get; set; }
    public Factura? Factura { get; set; }

    public Venta(int id, Cliente cliente, List<Vehiculo> vehiculos)
    {
        Id = id;
        Cliente = cliente;
        Vehiculos = vehiculos;
        Fecha = DateTime.Now;
    }

    public void GenerarFactura()
    {
        decimal total = 0;

        foreach (Vehiculo v in Vehiculos)
        {
            total += v.CalcularPrecioFinal();
            v.Vender(); // implementa IVendible
        }

        Factura = new Factura(Id, Fecha, Cliente, Vehiculos, total);
    }

    public string ToCsv()
    {
        string idsVehiculos = string.Join("|", Vehiculos.ConvertAll(v => v.Id.ToString()));
        decimal total = Factura?.Total ?? 0;
        return $"{Id},{Fecha:O},{Cliente.Cedula},{idsVehiculos},{total}";
    }

    public override string ToString()
    {
        return $"Venta #{Id} | Fecha: {Fecha:dd/MM/yyyy} | Cliente: {Cliente.Nombre} | Vehículos: {Vehiculos.Count} | Total: {Factura?.Total ?? 0:C}";
    }
}