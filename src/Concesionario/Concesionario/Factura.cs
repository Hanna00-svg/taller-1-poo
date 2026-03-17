public class Factura : IPersistible
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public Cliente Cliente { get; set; }
    public List<Vehiculo> Vehiculos { get; set; }
    public decimal Total { get; set; }

    public Factura(int id, DateTime fecha, Cliente cliente, List<Vehiculo> vehiculos, decimal total)
    {
        Id = id;
        Fecha = fecha;
        Cliente = cliente;
        Vehiculos = vehiculos;
        Total = total;
    }

    public string ToCsv()
    {
        // Formato: Id,Fecha,CedulaCliente,IdsVehiculos(separados por ;),Total
        string idsVehiculos = string.Join(";", Vehiculos.ConvertAll(v => v.Id.ToString()));
        return $"{Id},{Fecha:O},{Cliente.Cedula},{idsVehiculos},{Total}";
    }

    public override string ToString()
    {
        return $"Factura #{Id} | Fecha: {Fecha:dd/MM/yyyy} | Cliente: {Cliente.Nombre} | Total: {Total:C}";
    }
}