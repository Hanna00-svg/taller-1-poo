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
        using StreamWriter writer = new StreamWriter(_rutaArchivo, append: false);
        writer.WriteLine("Tipo,Id,Marca,Modelo,Color,Placa,Cilindraje,Precio,Vendido");
        foreach (Vehiculo v in Vehiculos)
            writer.WriteLine(v.ToCsv()); // usa el ToCsv() con discriminador de Carro/Moto
    }

    public void CargarVehiculos()
    {
        if (!File.Exists(_rutaArchivo))
            return;

        using StreamReader reader = new StreamReader(_rutaArchivo);
        reader.ReadLine(); // saltar encabezado

        Vehiculos.Clear();

        while (!reader.EndOfStream)
        {
            string? linea = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(linea)) continue;

            string[] d = linea.Split(',');
            if (d.Length < 9) continue;

            string tipo       = d[0];
            int id            = int.Parse(d[1]);
            string marca      = d[2];
            string modelo     = d[3];
            string color      = d[4];
            string placa      = d[5];
            int cilindraje    = int.Parse(d[6]);
            decimal precio    = decimal.Parse(d[7], System.Globalization.CultureInfo.InvariantCulture);
            bool vendido      = bool.Parse(d[8]);

            Vehiculo v = tipo == "Moto"
                ? new Moto(id, marca, modelo, color, placa, cilindraje, precio)
                : new Carro(id, marca, modelo, color, placa, cilindraje, precio);

            v.Vendido = vendido;
            Vehiculos.Add(v);
        }

        Console.WriteLine($"Se cargaron {Vehiculos.Count} vehículos exitosamente.");
    }
}