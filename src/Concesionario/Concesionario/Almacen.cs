using System.Data.Common;

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
        using (StreamWriter writer = new StreamWriter("vehiculos.csv"))
        {
            // 1. Encabezado con los atributos comunes
            writer.WriteLine("Id,Marca,Modelo,Color,Placa,Cilindraje,Precio");

            // 2. Ciclo for para recorrer la lista
            for (int i = 0; i < vehiculos.Count; i++)
            {
            Vehiculo v = vehiculos[i];
            
            // 3. Creamos la línea solo con las propiedades de la clase madre
            string linea = $"{v.Id},{v.Marca},{v.Modelo},{v.Color},{v.Placa},{v.Cilindraje},{v.Precio}";
            
            // 4. Escribimos en el archivo
            writer.WriteLine(linea);
            }
        }
    }

        public void CargarVehiculos()   
    {
        // 1. Verificamos si el archivo existe para evitar que el programa falle
        if (!File.Exists("vehiculos.csv"))
        {
        Console.WriteLine("No se encontró el archivo de datos.");
        return;
        }

        using (StreamReader reader = new StreamReader("vehiculos.csv"))
        {
        // 2. Saltamos la primera línea (el encabezado)
        reader.ReadLine();

        // 3. Limpiamos la lista actual para no duplicar datos si cargamos varias veces
        vehiculos.Clear();

            while (!reader.EndOfStream)
            {
                string linea = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(linea)) continue;

                // 4. Separamos los datos por la coma
                string[] datos = linea.Split(',');

                // 5. Convertimos los textos a sus tipos correspondientes (int, string, etc.)
                // Creamos un objeto 'Carro' por defecto para llenar la lista de Vehiculos
                Vehiculo v = new Carro(
                int.Parse(datos[0]), // Id
                datos[1],            // Marca
                datos[2],            // Modelo
                datos[3],            // Color
                datos[4],            // Placa
                int.Parse(datos[5]),  // Cilindraje
                decimal.Parse(datos[6])   //Precio
            );

            // 6. Agregamos a la lista global
            vehiculos.Add(v);
        }
        }
        Console.WriteLine($"Se cargaron {vehiculos.Count} vehículos exitosamente.");
    }
    
        
    

   
    
}