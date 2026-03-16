
namespace ConsoleApp;

public static class UIUsuarios
{
    private static List<Cliente> _clientes = Cliente.CargarClientes();

    public static List<Cliente> GetClientes() => _clientes;

    public static void SubmenuClientes()
    {
        string menu = """
        1. Crear Cliente
        2. Listar Clientes
        3. Actualizar Cliente
        4. Eliminar Cliente
        5. Volver
        """;

        while (true)
        {
            Console.WriteLine(menu);
            string opcion = Console.ReadLine()!;
            switch (opcion)
            {
                case "1": CrearCliente(); break;
                case "2": ListarClientes(); break;
                case "3": ActualizarCliente(); break;
                case "4": EliminarCliente(); break;
                case "5": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    static void CrearCliente()
    {
        Console.Write("Cédula: "); int cedula = int.Parse(Console.ReadLine()!);
        Console.Write("Nombre: "); string nombre = Console.ReadLine()!;
        Console.Write("Teléfono: "); string telefono = Console.ReadLine()!;
        Console.Write("Dirección: "); string direccion = Console.ReadLine()!;

        _clientes.Add(new Cliente { Cedula = cedula, Nombre = nombre, Telefono = telefono, Direccion = direccion });
        Cliente.GuardarClientes(_clientes);
        Console.WriteLine("""

        ================================================================================
        
        """);
    }

    static void ListarClientes()
    {
        if (_clientes.Count == 0) { Console.WriteLine("No hay clientes."); return; }
        foreach (var c in _clientes)
            Console.WriteLine($"{c.Cedula} - {c.Nombre} | Tel: {c.Telefono} | Dir: {c.Direccion}");
        Console.WriteLine("""

        ================================================================================
        
        """);
    }

    static void ActualizarCliente()
    {
        Console.Write("Cédula: "); int cedula = int.Parse(Console.ReadLine()!);
        var cliente = _clientes.FirstOrDefault(c => c.Cedula == cedula);
        if (cliente == null) { Console.WriteLine("No encontrado."); return; }

        Console.Write("Nuevo nombre: "); cliente.Nombre = Console.ReadLine()!;
        Console.Write("Nuevo teléfono: "); cliente.Telefono = Console.ReadLine()!;
        Console.Write("Nueva dirección: "); cliente.Direccion = Console.ReadLine()!;
        Cliente.GuardarClientes(_clientes);
        Console.WriteLine("""
        
        ================================================================================
        
        """);
    }

    static void EliminarCliente()
    {
        Console.Write("Cédula: "); int cedula = int.Parse(Console.ReadLine()!);
        var cliente = _clientes.FirstOrDefault(c => c.Cedula == cedula);
        if (cliente == null) { Console.WriteLine("No encontrado."); return; }
        _clientes.Remove(cliente);
        Cliente.GuardarClientes(_clientes);
        Console.WriteLine("""

        ================================================================================
        
        """);
    }
}
