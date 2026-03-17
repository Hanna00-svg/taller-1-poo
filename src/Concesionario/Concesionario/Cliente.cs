using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public class Cliente
{
    public long Cedula { get; set; }
    public string Nombre { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }

}
