using System.Data.Common;

public class Almacen
{
    public List<Vehiculo> vehiculos {get;set;}
    //agregamos el atributo rutaArchivo
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

   
    
}