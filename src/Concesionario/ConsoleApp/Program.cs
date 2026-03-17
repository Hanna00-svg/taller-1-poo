
using ConsoleApp;

string menu =
"""
 _______  _______  _        _______  _______  _______ _________ _______  _        _______  _______ _________ _______ 
(  ____ \(  ___  )( (    /|(  ____ \(  ____ \(  ____ \\__   __/(  ___  )( (    /|(  ___  )(  ____ )\__   __/(  ___  )
| (    \/| (   ) ||  \  ( || (    \/| (    \/| (    \/   ) (   | (   ) ||  \  ( || (   ) || (    )|   ) (   | (   ) |
| |      | |   | ||   \ | || |      | (__    | (_____    | |   | |   | ||   \ | || (___) || (____)|   | |   | |   | |
| |      | |   | || (\ \) || |      |  __)   (_____  )   | |   | |   | || (\ \) ||  ___  ||     __)   | |   | |   | |
| |      | |   | || | \   || |      | (            ) |   | |   | |   | || | \   || (   ) || (\ (      | |   | |   | |
| (____/\| (___) || )  \  || (____/\| (____/\/\____) |___) (___| (___) || )  \  || )   ( || ) \ \_____) (___| (___) |
(_______/(_______)|/    )_)(_______/(_______/\_______)\_______/(_______)|/    )_)|/     \||/   \__/\_______/(_______)


-----------------------------------------------------------------------------------------------------------------------
            1. Registrar Cliente
            2. Registrar Vehículo (Almacén)
            3. Registrar Venta
            4. Consultar Almacén
            5. Salir
-----------------------------------------------------------------------------------------------------------------------
            Ingrese una opción: "
""";

do
{
    Console.Write(menu);
    string? entrada = Console.ReadLine();

    switch (entrada)
    {
        case "1":
            UIUsuarios.SubmenuClientes();
            break;

        case "2":
            UIAlmacen.SubmenuAlmacen();
            break;

        case "3":
            UIVentas.SubmenuVentas();
            break;

        case "4":
            UIAlmacen.SubmenuAlmacen();
            break;

        case "5":
            Console.WriteLine("Saliendo del sistema...");
            return;

        default:
            Console.WriteLine("Opción inválida. Intente de nuevo.");
            break;
    }

    Console.WriteLine("\nPresione una tecla para continuar...");
    Console.ReadKey();

} while (true);