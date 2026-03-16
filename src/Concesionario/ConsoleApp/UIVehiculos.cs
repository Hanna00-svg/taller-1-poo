
namespace ConsoleApp;

public static class UIVehiculos
{
    private static List<Vehiculo> vehiculos = new List<Vehiculo>();

    public static void SubmenuVehiculos()
    {
        string menuVehiculos = """
        -------------------------------
        1. Crear Vehículo
        2. Listar Vehículos
        3. Actualizar Vehículo
        4. Eliminar Vehículo
        5. Volver
        -------------------------------
        Ingrese una opción: 
        """;

        do
        {
            Console.Write(menuVehiculos);
            string entrada = Console.ReadLine();

            switch (entrada)
            {
                case "1": CrearVehiculo(); break;
                case "2": ListarVehiculos(); break;
                case "3": ActualizarVehiculo(); break;
                case "4": EliminarVehiculo(); break;
                case "5": return;
                default: Console.WriteLine("Opción inválida."); break;
            }

            Console.WriteLine("\nPresione una tecla para continuar...");

        } while (true);
    }

    static void CrearVehiculo()
    {
        Console.WriteLine("=== CREAR VEHÍCULO ===");
        Console.WriteLine("1. Carro");
        Console.WriteLine("2. Moto");
        Console.Write("Seleccione tipo: ");
        string tipo = Console.ReadLine();

        Console.Write("Id: "); int id = int.Parse(Console.ReadLine());
        Console.Write("Marca: "); string marca = Console.ReadLine();
        Console.Write("Modelo: "); string modelo = Console.ReadLine();
        Console.Write("Color: "); string color = Console.ReadLine();
        Console.Write("Placa: "); string placa = Console.ReadLine();
        Console.Write("Cilindraje: "); int cilindraje = int.Parse(Console.ReadLine());
        Console.Write("Precio: "); decimal precio = decimal.Parse(Console.ReadLine());

        Vehiculo v = (tipo == "1")
            ? new Carro(id, marca, modelo, color, placa, cilindraje, precio)
            : new Moto(id, marca, modelo, color, placa, cilindraje, precio);

        vehiculos.Add(v);
        Console.WriteLine("\nVehículo registrado con éxito.");
        Console.WriteLine("""

        ================================================================================
        
        """);
    }

    static void ListarVehiculos()
    {
        Console.WriteLine("=== LISTA DE VEHÍCULOS ===");
        if (vehiculos.Count == 0) { Console.WriteLine("No hay vehículos registrados."); return; }
        foreach (var v in vehiculos)
            Console.WriteLine($"{v.Id} - {v.Marca} {v.Modelo} ({v.Placa}) Precio: {v.Precio} Vendido: {v.Vendido}");
        Console.WriteLine("""

        ================================================================================
        
        """);
    }

    static void ActualizarVehiculo()
    {
        Console.WriteLine("=== ACTUALIZAR VEHÍCULO ===");
        Console.Write("Ingrese el Id: "); long id = long.Parse(Console.ReadLine());
        var vehiculo = vehiculos.FirstOrDefault(v => v.Id == id);
        if (vehiculo == null) { Console.WriteLine("Vehículo no encontrado."); return; }

        Console.Write("Nueva marca: "); vehiculo.Marca = Console.ReadLine();
        Console.Write("Nuevo modelo: "); vehiculo.Modelo = Console.ReadLine();
        Console.Write("Nuevo color: "); vehiculo.Color = Console.ReadLine();
        Console.Write("Nueva placa: "); vehiculo.Placa = Console.ReadLine();
        Console.Write("Nuevo cilindraje: "); vehiculo.Cilindraje = int.Parse(Console.ReadLine());
        Console.Write("Nuevo precio: "); vehiculo.Precio = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Vehículo actualizado con éxito.");
        Console.WriteLine("""

        ================================================================================
        
        """);
    }

    static void EliminarVehiculo()
    {
        Console.WriteLine("=== ELIMINAR VEHÍCULO ===");
        Console.Write("Ingrese el Id: "); long id = long.Parse(Console.ReadLine());
        var vehiculo = vehiculos.FirstOrDefault(v => v.Id == id);
        if (vehiculo == null) { Console.WriteLine("Vehículo no encontrado."); return; }
        vehiculos.Remove(vehiculo);
        Console.WriteLine("Vehículo eliminado con éxito.");
        Console.WriteLine("""

        ================================================================================
        
        """);
    }
}
