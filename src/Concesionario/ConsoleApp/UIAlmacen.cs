
namespace ConsoleApp;

public static class UIAlmacen
{
    // Instancia compartida del almacén (usada también por UIVentas)
    private static Almacen _almacen = new Almacen();

    public static Almacen GetAlmacen() => _almacen;

    public static void SubmenuAlmacen()
    {
        string menuAlmacen = """
        -------------------------------
        1. Agregar Vehículo al Almacén
        2. Eliminar Vehículo del Almacén
        3. Consultar Disponibilidad
        4. Buscar por Placa
        5. Listar Vehículos
        6. Volver
        -------------------------------
        Ingrese una opción: 
        """;

        do
        {
            Console.Write(menuAlmacen);
            string? entrada = Console.ReadLine();

            switch (entrada)
            {
                case "1": AgregarVehiculo(); break;
                case "2": EliminarVehiculo(); break;
                case "3": ConsultarDisponibilidad(); break;
                case "4": BuscarPorPlaca(); break;
                case "5": ListarVehiculos(); break;
                case "6": return;
                default: Console.WriteLine("Opción inválida."); break;
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();

        } while (true);
    }

    static void AgregarVehiculo()
    {
        Console.WriteLine("=== AGREGAR VEHÍCULO AL ALMACÉN ===");
        Console.WriteLine("1. Carro");
        Console.WriteLine("2. Moto");
        Console.Write("Seleccione tipo: "); string? tipo = Console.ReadLine();

        Console.Write("Id: "); int id = int.Parse(Console.ReadLine()!);
        Console.Write("Marca: "); string marca = Console.ReadLine()!;
        Console.Write("Modelo: "); string modelo = Console.ReadLine()!;
        Console.Write("Color: "); string color = Console.ReadLine()!;
        Console.Write("Placa: "); string placa = Console.ReadLine()!;
        Console.Write("Cilindraje: "); int cilindraje = int.Parse(Console.ReadLine()!);
        Console.Write("Precio: "); decimal precio = decimal.Parse(Console.ReadLine()!);

        Vehiculo v = (tipo == "2")
            ? new Moto(id, marca, modelo, color, placa, cilindraje, precio)
            : new Carro(id, marca, modelo, color, placa, cilindraje, precio);

        _almacen.AgregarVehiculo(v); // guarda automáticamente
        Console.WriteLine($"Vehículo agregado al almacén con éxito. Precio final: {v.CalcularPrecioFinal():C}");
    }

    static void EliminarVehiculo()
    {
        Console.WriteLine("=== ELIMINAR VEHÍCULO DEL ALMACÉN ===");
        Console.Write("Ingrese el Id: "); int id = int.Parse(Console.ReadLine()!);
        _almacen.EliminarVehiculo(id); // guarda automáticamente
    }

    static void ConsultarDisponibilidad()
    {
        Console.WriteLine("=== CONSULTAR DISPONIBILIDAD ===");
        Console.Write("Ingrese el Id: "); int id = int.Parse(Console.ReadLine()!);
        bool disponible = _almacen.ConsultarDisponibilidad(id);
        Console.WriteLine(disponible ? "Vehículo disponible." : "Vehículo no disponible o ya vendido.");
    }

    static void BuscarPorPlaca()
    {
        Console.WriteLine("=== BUSCAR VEHÍCULO POR PLACA ===");
        Console.Write("Ingrese la placa: "); string? placa = Console.ReadLine();
        var v = _almacen.BuscarPorPlaca(placa!);
        if (v == null) Console.WriteLine("Vehículo no encontrado.");
        else Console.WriteLine($"{v.Id} - {v.Marca} {v.Modelo} ({v.Placa}) Precio final: {v.CalcularPrecioFinal():C} Vendido: {v.Vendido}");
    }

    static void ListarVehiculos()
    {
        Console.WriteLine("=== LISTA DE VEHÍCULOS EN ALMACÉN ===");
        if (_almacen.Vehiculos.Count == 0) { Console.WriteLine("No hay vehículos en el almacén."); return; }
        foreach (var v in _almacen.Vehiculos)
            Console.WriteLine($"[{v.Id}] {v.Marca} {v.Modelo} ({v.Placa}) | Precio final: {v.CalcularPrecioFinal():C} | Vendido: {v.Vendido}");
    }
}