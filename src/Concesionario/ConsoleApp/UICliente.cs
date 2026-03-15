

    namespace ConsoleApp;

    public static class UIUsuarios{

    private static List<Cliente> clientes = new List<Cliente>();

    
        static void SubmenuClientes()
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
                string entrada = Console.ReadLine();

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
            Console.Write("Cédula: "); int cedula = int.Parse(Console.ReadLine());
            Console.Write("Nombre: "); string nombre = Console.ReadLine();
            Console.Write("Teléfono: "); string telefono = Console.ReadLine();
            Console.Write("Dirección: "); string direccion = Console.ReadLine();

            clientes.Add(new Cliente(cedula, nombre, telefono, direccion));
            Console.WriteLine("\nCliente registrado con éxito.");
        }

        static void ListarClientes()
        {
            Console.WriteLine("=== LISTA DE CLIENTES ===");
            if (clientes.Count == 0) { Console.WriteLine("No hay clientes registrados."); return; }
            foreach (var c in clientes)
                Console.WriteLine($"Cédula: {c.Cedula}, Nombre: {c.Nombre}, Teléfono: {c.Telefono}, Dirección: {c.Direccion}");
        }

        static void ActualizarCliente()
        {
            Console.WriteLine("=== ACTUALIZAR CLIENTE ===");
            Console.Write("Ingrese la cédula: "); int cedula = int.Parse(Console.ReadLine());
            var cliente = clientes.FirstOrDefault(c => c.Cedula == cedula);
            if (cliente == null) { Console.WriteLine("Cliente no encontrado."); return; }

            Console.Write("Nuevo nombre: "); cliente.Nombre = Console.ReadLine();
            Console.Write("Nuevo teléfono: "); cliente.Telefono = Console.ReadLine();
            Console.Write("Nueva dirección: "); cliente.Direccion = Console.ReadLine();
            Console.WriteLine("Cliente actualizado con éxito.");
        }

        static void EliminarCliente()
        {
            Console.WriteLine("=== ELIMINAR CLIENTE ===");
            Console.Write("Ingrese la cédula: "); int cedula = int.Parse(Console.ReadLine());
            var cliente = clientes.FirstOrDefault(c => c.Cedula == cedula);
            if (cliente == null) { Console.WriteLine("Cliente no encontrado."); return; }
            clientes.Remove(cliente);
            Console.WriteLine("Cliente eliminado con éxito.");
        }

    }