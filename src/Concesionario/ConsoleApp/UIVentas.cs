namespace ConsoleApp;

public static class UIVentas
{
    private static readonly string rutaVentas = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ventas.csv");
    private static readonly PersistenciaCsv<Venta> persistencia = new PersistenciaCsv<Venta>();
    private static List<Venta> _ventas = persistencia.Cargar(rutaVentas);

    public static void SubmenuVentas()
    {
        string menu = """
        === SUBMENÚ VENTAS ===
        1. Crear Venta
        2. Listar Ventas
        3. Consultar Factura
        4. Eliminar Venta
        5. Volver
        """;

        while (true)
        {
            Console.WriteLine(menu);
            string opcion = Console.ReadLine()!;
            switch (opcion)
            {
                case "1": CrearVenta(); break;
                case "2": ListarVentas(); break;
                case "3": ConsultarFactura(); break;
                case "4": EliminarVenta(); break;
                case "5": return;
                default: Console.WriteLine("Opción inválida."); break;
            }
        }
    }

    static void CrearVenta()
    {
        Console.Write("Id Venta: "); long id = long.Parse(Console.ReadLine()!);
        Console.Write("Cédula Cliente: "); long cedula = long.Parse(Console.ReadLine()!);

        var cliente = UIUsuarios.GetClientes().FirstOrDefault(c => c.Cedula == cedula);
        if (cliente == null) { Console.WriteLine("Cliente no encontrado."); return; }

        Console.Write("Id Vehículo: "); long idVehiculo = long.Parse(Console.ReadLine()!);
        var vehiculo = UIVehiculos.GetVehiculos().FirstOrDefault(v => v.Id == idVehiculo && !v.Vendido);
        if (vehiculo == null) { Console.WriteLine("Vehículo no disponible."); return; }

        var venta = new Venta { Id = id, Fecha = DateTime.Now, Cliente = cliente, Vehiculos = new List<Vehiculo> { vehiculo } };
        venta.GenerarFactura();
        _ventas.Add(venta);
        persistencia.Guardar(_ventas, rutaVentas);

        Console.WriteLine($"Venta creada. Total: {venta.Factura?.Total}");
    }

    static void ListarVentas()
    {
        if (_ventas.Count == 0) { Console.WriteLine("No hay ventas."); return; }
        foreach (var v in _ventas)
            Console.WriteLine($"Venta {v.Id} - Cliente: {v.Cliente.Nombre} - Total: {v.Factura?.Total}");
    }

    static void ConsultarFactura()
    {
        Console.Write("Id Venta: "); int id = int.Parse(Console.ReadLine()!);
        var venta = _ventas.FirstOrDefault(v => v.Id == id);
        if (venta == null) { Console.WriteLine("Venta no encontrada."); return; }

        Console.WriteLine($"Factura {venta.Factura?.Id} - Fecha: {venta.Fecha}");
        Console.WriteLine($"Cliente: {venta.Cliente.Nombre}");
        Console.WriteLine("Vehículos:");
        foreach (var v in venta.Vehiculos)
            Console.WriteLine($"   {v.Marca} {v.Modelo} - Placa: {v.Placa} - Precio Final: {v.CalcularPrecioFinal()}");
        Console.WriteLine($"Total: {venta.Factura?.Total}");
    }
    
    static void EliminarVenta()
    {
        Console.Write("Id Venta: "); int id = int.Parse(Console.ReadLine()!);
        var venta = _ventas.FirstOrDefault(v => v.Id == id);
        if (venta == null) { Console.WriteLine("Venta no encontrada."); return; }
        _ventas.Remove(venta);
        persistencia.Guardar(_ventas, rutaVentas);
        Console.WriteLine("Venta eliminada.");
    }

    // Método auxiliar
    public static List<Venta> GetVentas() => _ventas;
}


