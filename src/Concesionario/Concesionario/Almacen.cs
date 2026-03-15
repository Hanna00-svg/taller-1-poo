using System.Data.Common;
using System.Globalization;
using CsvHelper;
public class Almacen
{
    public List<Vehiculo> vehiculos {get;set;}
   
    private string rutaArchivo = "vehiculos.csv";
    public Almacen(){
        
    }

    public void AgregarVehiculo(Vehiculo vehiculo)
    {
        
        vehiculos.Add(vehiculo);
    }

    public void EliminarVehiculo(int id)
    {
        Vehiculo vehiculoEliminar = vehiculos.Find(v => v.Id == id);
        if (vehiculoEliminar != null)
        {
            vehiculos.Remove(vehiculoEliminar);
            Console.WriteLine($"Vehículo con ID {id} ha sido eliminado.");
        }
        else
        {
            Console.WriteLine($"No se encontró ningún vehículo con el ID {id}.");
        }
    }

    public bool ConsultarDisponibilida(int id)
    {
      bool existe =  vehiculos.Any(v => v.Id == id);
      return existe;
    }

   public void GuardarVehiculos()
    {
        // Usamos CultureInfo.InvariantCulture para asegurar que los decimales 
        // se guarden con punto (.) y no dependan del idioma del sistema.
            using (var writer = new StreamWriter("vehiculos.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
            // Escribe toda la lista de un solo golpe, incluyendo el encabezado
            csv.WriteRecords(vehiculos);
            }
    }

       public void CargarVehiculos()
    {
        if (!File.Exists("vehiculos.csv"))
        {
            Console.WriteLine("No se encontró el archivo de datos.");
            return;
        }

        using (var reader = new StreamReader("vehiculos.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            // Limpiamos la lista para evitar duplicados
            vehiculos.Clear();
        
            // CsvHelper mapea automáticamente las columnas a las propiedades de la clase Carro
            // .ToList() ejecuta la lectura de todas las filas
            var registros = csv.GetRecords<Carro>().ToList();
        
            foreach (var v in registros)
            {
                vehiculos.Add(v);
            }
    }
    
    Console.WriteLine($"Se cargaron {vehiculos.Count} vehículos exitosamente.");
}
        
    

   
    
}