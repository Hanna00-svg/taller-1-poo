namespace ConsoleApp;

public static class UIVentas
{
    private static List<Venta> _ventas = new List<Venta>();
    private static readonly string _rutaVentas = "ventas.csv";

    static UIVentas()
    {
        // Nota: cargar ventas completas desde CSV requeriría referenciar
        // clientes y vehículos, lo cual se gestiona en memoria en esta versión.
        // El CSV de ventas se usa sólo para auditoría/histórico.
    }

    public static void SubmenuVentas()
    {
        string menuVentas = """
        -------------------------------
        1. Crear Venta
        2. Listar Ventas
        3. Consultar Factura
        4. Eliminar Venta
        5. Volver
        -------------------------------
        Ingrese una opción: 
        """;

        do
        {
            Console.Write(menuVentas);
            string? entrada = Console.ReadLine();

            switch (entrada)
            {
                case "1": CrearVenta(); break;
                case "2": ListarVentas(); break;
                case "3": ConsultarFactura(); break;
                case "4": EliminarVenta(); break;
                case "5": return;
                default: Console.WriteLine("Opción inválida."); break;
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();

        } while (true);
    }

    static void CrearVenta()
    {
        Console.WriteLine("=== CREAR VENTA ===");
        Console.Write("Id de la venta: "); int id = int.Parse(Console.ReadLine()!);

        // 1. Buscar cliente
        Console.Write("Cédula del cliente: "); int cedula = int.Parse(Console.ReadLine()!);
        var cliente = UIUsuarios.GetClientes().FirstOrDefault(c => c.Cedula == cedula);
        if (cliente == null) { Console.WriteLine("Cliente no encontrado."); return; }

        // 2. Seleccionar vehículos del almacén disponibles
        Almacen almacen = UIAlmacen.GetAlmacen();
        List<Vehiculo> vehiculosDisponibles = almacen.Vehiculos.FindAll(v => !v.Vendido);

        if (vehiculosDisponibles.Count == 0)
        {
            Console.WriteLine("No hay vehículos disponibles en el almacén.");
            return;
        }

        Console.WriteLine("\n=== VEHÍCULOS DISPONIBLES ===");
        foreach (var v in vehiculosDisponibles)
            Console.WriteLine($"  [{v.Id}] {v.Marca} {v.Modelo} - Placa: {v.Placa} - Precio base: {v.Precio:C} - Precio final: {v.CalcularPrecioFinal():C}");

        // 3. El usuario elige qué vehículos incluir (por Id, separados por coma)
        Console.Write("\nIngrese los Id de vehículos a incluir (separados por coma): ");
        string? idsInput = Console.ReadLine();
        List<Vehiculo> vehiculosVenta = new List<Vehiculo>();

        if (!string.IsNullOrWhiteSpace(idsInput))
        {
            foreach (string parte in idsInput.Split(','))
            {
                if (int.TryParse(parte.Trim(), out int vid))
                {
                    Vehiculo? veh = vehiculosDisponibles.FirstOrDefault(v => v.Id == vid);
                    if (veh != null) vehiculosVenta.Add(veh);
                    else Console.WriteLine($"  Vehículo con Id {vid} no encontrado o no disponible, ignorado.");
                }
            }
        }

        if (vehiculosVenta.Count == 0)
        {
            Console.WriteLine("No se seleccionó ningún vehículo válido. Venta cancelada.");
            return;
        }

        // 4. Crear venta y generar factura (marca vehículos como vendidos)
        Venta venta = new Venta(id, cliente, vehiculosVenta);
        venta.GenerarFactura();
        _ventas.Add(venta);

        // 5. Persistir: guardar el almacén (vehículos marcados como vendidos) y el CSV de ventas
        almacen.GuardarVehiculos();
        GuardarVentas();

        Console.WriteLine($"\n✔ Venta registrada. {venta.Factura!}");
    }

    static void ListarVentas()
    {
        Console.WriteLine("=== LISTA DE VENTAS ===");
        if (_ventas.Count == 0) { Console.WriteLine("No hay ventas registradas."); return; }
        foreach (var v in _ventas)
            Console.WriteLine(v);
    }

    static void ConsultarFactura()
    {
        Console.WriteLine("=== CONSULTAR FACTURA ===");
        Console.Write("Ingrese Id de la venta: "); int id = int.Parse(Console.ReadLine()!);
        var venta = _ventas.FirstOrDefault(v => v.Id == id);
        if (venta == null) { Console.WriteLine("Venta no encontrada."); return; }

        Console.WriteLine($"\n{venta.Factura}");
        Console.WriteLine("Vehículos:");
        foreach (var v in venta.Factura!.Vehiculos)
            Console.WriteLine($"  - {v.Marca} {v.Modelo} ({v.Placa}) - Precio final: {v.CalcularPrecioFinal():C}");
    }

    static void EliminarVenta()
    {
        Console.WriteLine("=== ELIMINAR VENTA ===");
        Console.Write("Ingrese Id de la venta: "); int id = int.Parse(Console.ReadLine()!);
        var venta = _ventas.FirstOrDefault(v => v.Id == id);
        if (venta == null) { Console.WriteLine("Venta no encontrada."); return; }
        _ventas.Remove(venta);
        GuardarVentas();
        Console.WriteLine("Venta eliminada con éxito.");
    }

    // ── Persistencia CSV (histórico) ─────────────────────────────────────────

    static void GuardarVentas()
    {
        using StreamWriter w = new StreamWriter(_rutaVentas, append: false);
        w.WriteLine("Id,Fecha,CedulaCliente,IdsVehiculos,Total");
        foreach (var v in _ventas)
            w.WriteLine(v.ToCsv());
    }
}