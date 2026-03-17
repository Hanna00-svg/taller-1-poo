using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

/// <summary>
/// Repositorio genérico de persistencia CSV usando CsvHelper.
/// Permite guardar y cargar cualquier tipo de objeto con cabecera automática.
/// </summary>
public static class CsvRepo
{
    private static readonly CsvConfiguration _config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
    };

    /// <summary>Guarda una lista de objetos en un archivo CSV, sobreescribiendo si existe.</summary>
    public static void Guardar<T>(string ruta, IEnumerable<T> datos)
    {
        using StreamWriter writer = new StreamWriter(ruta, append: false);
        using CsvWriter csv = new CsvWriter(writer, _config);
        csv.WriteRecords(datos);
    }

    /// <summary>Carga una lista de objetos desde un archivo CSV. Retorna lista vacía si no existe.</summary>
    public static List<T> Cargar<T>(string ruta)
    {
        if (!File.Exists(ruta))
            return new List<T>();

        using StreamReader reader = new StreamReader(ruta);
        using CsvReader csv = new CsvReader(reader, _config);
        return csv.GetRecords<T>().ToList();
    }
}