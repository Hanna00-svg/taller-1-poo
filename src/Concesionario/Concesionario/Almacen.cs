using System.Globalization;
using CsvHelper;

public class Almacen
{
    public List<Vehiculo> Vehiculos { get; set; }
    private readonly string _rutaArchivo = "vehiculos.csv";

    public Almacen()
    {
        // Fix: inicializar la lista para evitar NullReferenceException
        Vehiculos = new List<Vehiculo>();
        CargarVehiculos();
    }

    public void AgregarVehiculo(Vehiculo vehiculo)
    {
        Vehiculos.Add(vehiculo);
        GuardarVehiculos();
    }

    public void EliminarVehiculo(int id)
    {
        Vehiculo? vehiculoEliminar = Vehiculos.Find(v => v.Id == id);
        if (vehiculoEliminar != null)
        {
            Vehiculos.Remove(vehiculoEliminar);
            GuardarVehiculos();
            Console.WriteLine($"Vehículo con ID {id} ha sido eliminado.");
        }
        else
        {
            Console.WriteLine($"No se encontró ningún vehículo con el ID {id}.");
        }
    }

    public bool ConsultarDisponibilidad(int id)
    {
        return Vehiculos.Any(v => v.Id == id && !v.Vendido);
    }

    public Vehiculo? BuscarPorPlaca(string placa)
    {
        return Vehiculos.FirstOrDefault(v => v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
    }

    // ── Persistencia con CSV manual (formato: Tipo,Id,Marca,Modelo,Color,Placa,Cilindraje,Precio,Vendido) ──

   public void GuardarVehiculos()
    {
        using (var writer = new StreamWriter("vehiculos.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(Vehiculos);
        }
    }
    public void CargarVehiculos()
    {
        if (!File.Exists("vehiculos.csv")) return;

        using (var reader = new StreamReader("vehiculos.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            Vehiculos.Clear();
            // Leemos como Carro (o la clase que estés usando para el mapeo)
            var registros = csv.GetRecords<Carro>().ToList();
            Vehiculos.AddRange(registros);
        }
    }
}