
namespace ConsoleApp;

public static class UIVentas
{
    private static List<Venta> _ventas = Venta.CargarVentas(UIUsuarios.GetClientes(), UIAlmacen.GetAlmacen());

    public static void SubmenuVentas()
    {
        string menu = """
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
        Console.Write("Id de venta: "); int id = int.Parse(Console.ReadLine()!);
        Console.Write("Cédula cliente: "); int cedula = int.Parse(Console.ReadLine()!);

        var cliente = UIUsuarios.GetClientes().FirstOrDefault(c => c.Cedula == cedula);
        if (cliente == null) { Console.WriteLine("Cliente no encontrado."); return; }

        var almacen = UIAlmacen.GetAlmacen();
        var disponibles = almacen.Vehiculos.Where(v => !v.Vendido).ToList();
        if (disponibles.Count == 0) { Console.WriteLine("No hay vehículos disponibles."); return; }

        Console.WriteLine("Vehículos disponibles:");
        foreach (var v in disponibles)
            Console.WriteLine($"[{v.Id}] {v.Marca} {v.Modelo} - {v.Placa}");

        Console.Write("Ids seleccionados (coma): ");
        var ids = Console.ReadLine()!.Split(',').Select(int.Parse).ToList();
        var vehiculos = disponibles.Where(v => ids.Contains(v.Id)).ToList();

        var venta = new Venta { Id = id, Cliente = cliente, Vehiculos = vehiculos, Fecha = DateTime.Now };
        venta.GenerarFactura();
        _ventas.Add(venta);

        Venta.GuardarVentas(_ventas);
        almacen.GuardarVehiculos();
    }

    static void ListarVentas()
{
    Console.WriteLine("=== LISTA DE VENTAS ===");
    if (_ventas.Count == 0)
    {
        Console.WriteLine("No hay ventas registradas.");
        return;
    }

    foreach (var v in _ventas)
    {
        Console.WriteLine($"Venta #{v.Id} | Fecha: {v.Fecha:d} | Cliente: {v.Cliente.Nombre} | Total: {v.Factura?.Total:C}");
    }
}

    static void ConsultarFactura()
{
    Console.WriteLine("=== CONSULTAR FACTURA ===");
    Console.Write("Ingrese Id de la venta: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Id inválido.");
        return;
    }

    var venta = _ventas.FirstOrDefault(v => v.Id == id);
    if (venta == null)
    {
        Console.WriteLine("Venta no encontrada.");
        return;
    }

    var factura = venta.Factura;
    if (factura == null)
    {
        Console.WriteLine("La venta no tiene factura generada.");
        return;
    }

    Console.WriteLine($"\nFactura #{factura.Id} - Fecha: {factura.Fecha:d}");
    Console.WriteLine($"Cliente: {factura.Cliente.Nombre} (Cédula: {factura.Cliente.Cedula})");
    Console.WriteLine($"Total: {factura.Total:C}");
    Console.WriteLine("Vehículos:");
    foreach (var v in factura.Vehiculos)
        Console.WriteLine($"  - {v.Marca} {v.Modelo} ({v.Placa}) - Precio final: {v.CalcularPrecioFinal():C}");
    }

    static void EliminarVenta()
    {
    Console.WriteLine("=== ELIMINAR VENTA ===");
    Console.Write("Ingrese Id de la venta: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Id inválido.");
        return;
    }

    var venta = _ventas.FirstOrDefault(v => v.Id == id);
    if (venta == null)
    {
        Console.WriteLine("Venta no encontrada.");
        return;
    }

    _ventas.Remove(venta);
    Venta.GuardarVentas(_ventas);   // persistencia
    Console.WriteLine("Venta eliminada con éxito.");
    }


}

