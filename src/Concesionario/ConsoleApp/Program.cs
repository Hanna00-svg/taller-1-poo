using ConsoleApp;

string menu =
" _______  _______  _        _______  _______  _______ _________ _______  _        _______  _______ _________ _______ \n" +
"(  ____ \\(  ___  )( (    /|(  ____ \\(  ____ \\(  ____ \\\\__   __/(  ___  )( (    /|(  ___  )(  ____ )\\__   __/(  ___  )\n" +
"| (    \\/| (   ) ||  \\  ( || (    \\/| (    \\/| (    \\/   ) (   | (   ) ||  \\  ( || (   ) || (    )|   ) (   | (   ) |\n" +
"| |      | |   | ||   \\ | || |      | (__    | (_____    | |   | |   | ||   \\ | || (___) || (____)|   | |   | |   | |\n" +
"| |      | |   | || (\\ \\) || |      |  __)   (_____  )   | |   | |   | || (\\ \\) ||  ___  ||     __)   | |   | |   | |\n" +
"| |      | |   | || | \\   || |      | (            ) |   | |   | |   | || | \\   || (   ) || (\\ (      | |   | |   | |\n" +
"| (____/\\| (___) || )  \\  || (____/\\| (____/\\/\\____) |___) (___| (___) || )  \\  || )   ( || ) \\ \\_____) (___| (___) |\n" +
"(_______/(_______)|/    )_)(_______/(_______/\\_______)\\_______/(_______)|/    )_)|/     \\||/   \\__/\\_______/(_______)\n" +
"\n" +
"-----------------------------------------------------------------------------------------------------------------------\n" +
"            1. Registrar Cliente\n" +
"            2. Registrar Vehículo (Almacén)\n" +
"            3. Registrar Venta\n" +
"            4. Consultar Almacén\n" +
"            5. Salir\n" +
"-----------------------------------------------------------------------------------------------------------------------\n" +
"            Ingrese una opción: ";

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
