using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public class Cliente
{
    public int Cedula { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }

    private static readonly string _rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "clientes.csv");

    private static CsvConfiguration Config => new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower(),
        HeaderValidated = null,
        MissingFieldFound = null
    };

    public static void GuardarClientes(List<Cliente> clientes)
    {
        using var writer = new StreamWriter(_rutaArchivo);
        using var csv = new CsvWriter(writer, Config);
        csv.WriteRecords(clientes);
    }

    public static List<Cliente> CargarClientes()
    {
        if (!File.Exists(_rutaArchivo)) return new List<Cliente>();
        using var reader = new StreamReader(_rutaArchivo);
        using var csv = new CsvReader(reader, Config);
        return csv.GetRecords<Cliente>().ToList();
    }
}
