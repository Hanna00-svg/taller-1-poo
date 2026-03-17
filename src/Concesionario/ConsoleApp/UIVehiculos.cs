
namespace ConsoleApp;

public static class UIVehiculos
{
    private static readonly string rutaVehiculos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vehiculos.csv");
    private static readonly PersistenciaCsv<Vehiculo> persistencia = new PersistenciaCsv<Vehiculo>();
    private static List<Vehiculo> _vehiculos = CargarVehiculos();

    public static void SubmenuVehiculos()
    {
        string menu = """
        === SUBMENÚ VEHÍCULOS ===
        1. Agregar Vehículo
        2. Listar Vehículos
        3. Eliminar Vehículo
        4. Volver
        """;

        while (true)
        {
            Console.WriteLine(menu);
            string opcion = Console.ReadLine()!;
            switch (opcion)
            {
                case "1": AgregarVehiculo(); break;
                case "2": ListarVehiculos(); break;
                case "3": EliminarVehiculo(); break;
                case "4": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    static void AgregarVehiculo()
    {
        Console.Write("Id: "); long id = long.Parse(Console.ReadLine()!);
        Console.Write("Marca: "); string marca = Console.ReadLine()!;
        Console.Write("Modelo: "); string modelo = Console.ReadLine()!;
        Console.Write("Color: "); string color = Console.ReadLine()!;
        Console.Write("Placa: "); string placa = Console.ReadLine()!;
        Console.Write("Precio: "); decimal precio = decimal.Parse(Console.ReadLine()!);
        Console.Write("Cilindraje: "); int cilindraje = int.Parse(Console.ReadLine()!);

        Console.Write("Tipo (1=Carro, 2=Moto): ");
        string tipo = Console.ReadLine()!;
        Vehiculo v;

        if (tipo == "1")
        {
            v = new Carro(id, marca, modelo, color, placa, precio, cilindraje);
            v.Tipo = "Carro";
        }
        else
        {
            v = new Moto(id, marca, modelo, color, placa, precio, cilindraje);
            v.Tipo = "Moto";
        }
        _vehiculos.Add(v);
        persistencia.Guardar(_vehiculos, rutaVehiculos);
    }

    static void ListarVehiculos()
    {
        if (_vehiculos.Count == 0) { Console.WriteLine("No hay vehículos."); return; }
        foreach (var v in _vehiculos)
            Console.WriteLine($"{v.Id} - {v.Marca} {v.Modelo} | Placa: {v.Placa} | Precio: {v.Precio} | Cilindraje: {v.Cilindraje} | Vendido: {v.Vendido}");
    }

    static void EliminarVehiculo()
    {
        Console.Write("Id: "); long id = long.Parse(Console.ReadLine()!);
        var vehiculo = _vehiculos.FirstOrDefault(v => v.Id == id);
        if (vehiculo == null) { Console.WriteLine("No encontrado."); return; }
        _vehiculos.Remove(vehiculo);
        persistencia.Guardar(_vehiculos, rutaVehiculos);
    }

    private static List<Vehiculo> CargarVehiculos()
{
    var registros = persistencia.Cargar(rutaVehiculos);
    var lista = new List<Vehiculo>();

    foreach (var r in registros)
    {
        Vehiculo v;

        if (r.Tipo == "Carro")
            v = new Carro(r.Id, r.Marca, r.Modelo, r.Color, r.Placa, r.Precio, r.Cilindraje, r.Vendido);
        else
            v = new Moto(r.Id, r.Marca, r.Modelo, r.Color, r.Placa, r.Precio, r.Cilindraje, r.Vendido);

        v.Tipo = r.Tipo;
        lista.Add(v);
    }

    return lista;
}

    // Método auxiliar
    public static List<Vehiculo> GetVehiculos() => _vehiculos;
}
