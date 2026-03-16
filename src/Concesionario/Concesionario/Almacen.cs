using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public class Almacen
{
    public List<Vehiculo> Vehiculos { get; set; }
    private readonly string _rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vehiculos.csv");

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

    public Vehiculo BuscarPorPlaca(string placa)
    {
        return Vehiculos.FirstOrDefault(v => v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
    }

    
    // ── Persistencia con CSV manual (formato: Tipo,Id,Marca,Modelo,Color,Placa,Cilindraje,Precio,Vendido) ──

    private CsvConfiguration Config => new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        // Convierte todo a minúsculas antes de comparar, así 'Id' coincide con 'id'
        PrepareHeaderForMatch = args => args.Header.ToLower(),
        HeaderValidated = null, // Evita errores si falta alguna columna
        MissingFieldFound = null // Evita errores si hay campos vacíos
    };

    public void GuardarVehiculos()
    {
        using (var writer = new StreamWriter(_rutaArchivo))
        // CAMBIO: Usa 'Config' en lugar de 'CultureInfo.InvariantCulture'
        using (var csv = new CsvWriter(writer, Config)) 
    {
        csv.WriteRecords(Vehiculos);
    }
    }
    public void CargarVehiculos()
    {
        if (!File.Exists(_rutaArchivo)) return;

        using (var reader = new StreamReader(_rutaArchivo))
        // CAMBIO: Usa 'Config' en lugar de 'CultureInfo.InvariantCulture'
        using (var csv = new CsvReader(reader, Config)) 
        {
            Vehiculos.Clear();
            // Cargamos como Carro (esto funcionará para ambos si tienen los mismos campos)
            var registros = csv.GetRecords<Carro>().Cast<Vehiculo>().ToList();
            Vehiculos.AddRange(registros);
        }
    }
}