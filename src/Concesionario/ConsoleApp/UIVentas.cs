
namespace ConsoleApp;

public static class UIVentas
{
    private static List<Venta> ventas = new List<Venta>();

    static void SubmenuVentas()
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
            string entrada = Console.ReadLine();

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

        } while (true);
    }

    static void CrearVenta()
    {
        Console.WriteLine("=== CREAR VENTA ===");
        Console.Write("Id de la venta: "); int id = int.Parse(Console.ReadLine());
        Console.Write("Cédula del cliente: "); int cedula = int.Parse(Console.ReadLine());

        var cliente = UIUsuarios.GetClientes().FirstOrDefault(c => c.Cedula == cedula);
        if (cliente == null) { Console.WriteLine("Cliente no encontrado."); return; }

        // Aquí se puede pedir Ids de vehículos y agregarlos
        List<Vehiculo> vehiculos = new List<Vehiculo>();

        Venta venta = new Venta(id, cliente, vehiculos);
        venta.GenerarFactura();
        ventas.Add(venta);

        Console.WriteLine("Venta registrada con éxito.");
    }

    static void ListarVentas()
    {
        Console.WriteLine("=== LISTA DE VENTAS ===");
        if (ventas.Count == 0) { Console.WriteLine("No hay ventas registradas."); return; }
        foreach (var v in ventas)
            Console.WriteLine($"Venta {v.Id} - Cliente: {v.Cliente.Nombre} - Total: {v.Factura.Total}");
    }

    static void ConsultarFactura()
    {
        Console.WriteLine("=== CONSULTAR FACTURA ===");
        Console.Write("Ingrese Id de la venta: "); int id = int.Parse(Console.ReadLine());
        var venta = ventas.FirstOrDefault(v => v.Id == id);
        if (venta == null) { Console.WriteLine("Venta no encontrada."); return; }

        Console.WriteLine($"Factura {venta.Factura.Id} - Cliente: {venta.Cliente.Nombre} - Total: {venta.Factura.Total}");
    }

    static void EliminarVenta()
    {
        Console.WriteLine("=== ELIMINAR VENTA ===");
        Console.Write("Ingrese Id de la venta: "); int id = int.Parse(Console.ReadLine());
        var venta = ventas.FirstOrDefault(v => v.Id == id);
        if (venta == null) { Console.WriteLine("Venta no encontrada."); return; }
        ventas.Remove(venta);
        Console.WriteLine("Venta eliminada con éxito.");
    }
}
