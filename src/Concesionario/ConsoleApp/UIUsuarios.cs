
namespace ConsoleApp;

public static class UIUsuarios
{
    private static List<Cliente> _clientes = new List<Cliente>(); // Almacena los clientes en memoria
    private static readonly string _rutaClientes = "clientes.csv"; //Define la ruta

    static UIUsuarios()
    {
        CargarClientes();
    }

    // Expuesto para que UIVentas pueda buscar clientes por cédula
    public static List<Cliente> GetClientes() => _clientes;

    public static void SubmenuClientes()
    {
        string menuClientes = """
        -------------------------------
        1. Crear Cliente
        2. Listar Clientes
        3. Actualizar Cliente
        4. Eliminar Cliente
        5. Volver
        -------------------------------
        Ingrese una opción: 
        """;

        do
        {
            Console.Clear();
            Console.Write(menuClientes);
            string? entrada = Console.ReadLine();

            switch (entrada)
            {
                case "1": CrearCliente(); break;
                case "2": ListarClientes(); break;
                case "3": ActualizarCliente(); break;
                case "4": EliminarCliente(); break;
                case "5": return;
                default: Console.WriteLine("Opción inválida."); break;
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();

        } while (true);
    }

    static void CrearCliente()
    {
        Console.WriteLine("=== CREAR CLIENTE ===");
        Console.Write("Cédula: "); int cedula = int.Parse(Console.ReadLine()!);
        Console.Write("Nombre: "); string nombre = Console.ReadLine()!;
        Console.Write("Teléfono: "); string telefono = Console.ReadLine()!;
        Console.Write("Dirección: "); string direccion = Console.ReadLine()!;

        _clientes.Add(new Cliente(cedula, nombre, telefono, direccion));
        GuardarClientes();
        Console.WriteLine("\nCliente registrado con éxito.");
    }

    static void ListarClientes()
    {
        Console.WriteLine("=== LISTA DE CLIENTES ===");
        if (_clientes.Count == 0) { Console.WriteLine("No hay clientes registrados."); return; }
        foreach (var c in _clientes)
            Console.WriteLine($"Cédula: {c.Cedula}, Nombre: {c.Nombre}, Teléfono: {c.Telefono}, Dirección: {c.Direccion}");
    }

    static void ActualizarCliente()
    {
        Console.WriteLine("=== ACTUALIZAR CLIENTE ===");
        Console.Write("Ingrese la cédula: "); int cedula = int.Parse(Console.ReadLine()!);
        var cliente = _clientes.FirstOrDefault(c => c.Cedula == cedula);
        if (cliente == null) { Console.WriteLine("Cliente no encontrado."); return; }

        Console.Write("Nuevo nombre: "); cliente.Nombre = Console.ReadLine()!;
        Console.Write("Nuevo teléfono: "); cliente.Telefono = Console.ReadLine()!;
        Console.Write("Nueva dirección: "); cliente.Direccion = Console.ReadLine()!;
        GuardarClientes();
        Console.WriteLine("Cliente actualizado con éxito.");
    }

    static void EliminarCliente()
    {
        Console.WriteLine("=== ELIMINAR CLIENTE ===");
        Console.Write("Ingrese la cédula: "); int cedula = int.Parse(Console.ReadLine()!);
        var cliente = _clientes.FirstOrDefault(c => c.Cedula == cedula);
        if (cliente == null) { Console.WriteLine("Cliente no encontrado."); return; }
        _clientes.Remove(cliente);
        GuardarClientes();
        Console.WriteLine("Cliente eliminado con éxito.");
    }

    // ── Persistencia CSV ──────────────────────────────────────────────────────

    static void GuardarClientes()
    {
        using StreamWriter w = new StreamWriter(_rutaClientes, append: false); // abre el archivo para sobrescribir, no agrega al final
        w.WriteLine("Cedula,Nombre,Telefono,Direccion");
        foreach (var c in _clientes)
            w.WriteLine($"{c.Cedula},{c.Nombre},{c.Telefono},{c.Direccion}");
    }

    static void CargarClientes()
    {
        if (!File.Exists(_rutaClientes)) return; //si el archivo no existe, no hay nada que cargar

        using StreamReader r = new StreamReader(_rutaClientes);
        r.ReadLine(); // encabezado
        while (!r.EndOfStream)
        {
            string? linea = r.ReadLine();
            if (string.IsNullOrWhiteSpace(linea)) continue;
            string[] d = linea.Split(',');
            if (d.Length < 4) continue; // si no tiene 4 columnas se descarta
            _clientes.Add(new Cliente(int.Parse(d[0]), d[1], d[2], d[3])); // Se crea nuevo cliente con los datos leidos
        }
    }
}