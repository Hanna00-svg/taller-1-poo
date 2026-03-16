using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public Cliente Cliente { get; set; }
    public List<Vehiculo> Vehiculos { get; set; }
    public Factura? Factura { get; set; }

    private static readonly string _rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ventas.csv");

    private static CsvConfiguration Config => new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower(),
        HeaderValidated = null,
        MissingFieldFound = null
    };

    public void GenerarFactura()
    {
        decimal total = Vehiculos.Sum(v => v.CalcularPrecioFinal());
        foreach (var v in Vehiculos) v.Vendido = true;
        Factura = new Factura(Id, Fecha, Cliente, Vehiculos, total);
    }

    public static void GuardarVentas(List<Venta> ventas)
    {
        using var writer = new StreamWriter(_rutaArchivo);
        using var csv = new CsvWriter(writer, Config);
        csv.WriteRecords(ventas.Select(v => new
        {
            v.Id,
            v.Fecha,
            CedulaCliente = v.Cliente.Cedula,
            IdsVehiculos = string.Join("|", v.Vehiculos.Select(x => x.Id)),
            Total = v.Factura?.Total ?? 0
        }));
    }

    public static List<Venta> CargarVentas(List<Cliente> clientes, Almacen almacen)
    {
        if (!File.Exists(_rutaArchivo)) return new List<Venta>();
        using var reader = new StreamReader(_rutaArchivo);
        using var csv = new CsvReader(reader, Config);

        var registros = csv.GetRecords<dynamic>().ToList();
        var ventas = new List<Venta>();

        foreach (var r in registros)
        {
            int id = int.Parse(r.Id);
            DateTime fecha = DateTime.Parse(r.Fecha);
            int cedulaCliente = int.Parse(r.CedulaCliente);
            string[] idsVehiculos = r.IdsVehiculos.Split('|');
            decimal total = decimal.Parse(r.Total);

            var cliente = clientes.FirstOrDefault(c => c.Cedula == cedulaCliente);
            if (cliente == null) continue;

            var vehiculos = almacen.Vehiculos.Where(v => idsVehiculos.Contains(v.Id.ToString())).ToList();
            if (vehiculos.Count == 0) continue;

            var venta = new Venta { Id = id, Fecha = fecha, Cliente = cliente, Vehiculos = vehiculos };
            venta.Factura = new Factura(id, fecha, cliente, vehiculos, total);

            ventas.Add(venta);
        }

        return ventas;
    }
}

