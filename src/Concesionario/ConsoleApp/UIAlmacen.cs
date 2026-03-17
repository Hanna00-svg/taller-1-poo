
namespace ConsoleApp;

    public static class UIAlmacen
    {
    private static readonly string rutaVehiculos = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vehiculos.csv");
    private static readonly PersistenciaCsv<Vehiculo> persistencia = new PersistenciaCsv<Vehiculo>();
    private static List<Vehiculo> _vehiculos = persistencia.Cargar(rutaVehiculos);

    public static void SubmenuAlmacen()
    {
        string menu = """
        === SUBMENÚ ALMACÉN ===
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
        Vehiculo v = tipo == "1"
            ? new Carro(id, marca, modelo, color, placa, precio, cilindraje)
            : new Moto(id, marca, modelo, color, placa, precio, cilindraje);

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
}

