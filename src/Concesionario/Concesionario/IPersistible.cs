using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public interface IPersistible<T>
{
    void Guardar(List<T> elementos, string rutaArchivo);
    List<T> Cargar(string rutaArchivo);
}

public class PersistenciaCsv<T> : IPersistible<T>
{
    private CsvConfiguration Config => new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower(),
        HeaderValidated = null,
        MissingFieldFound = null
    };
public void Guardar(List<T> elementos, string rutaArchivo)
    {
        using var writer = new StreamWriter(rutaArchivo);
        using var csv = new CsvWriter(writer, Config);
        csv.WriteRecords(elementos);
    }

    public List<T> Cargar(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo)) return new List<T>();
        using var reader = new StreamReader(rutaArchivo);
        using var csv = new CsvReader(reader, Config);
        return csv.GetRecords<T>().ToList();
    }
}
