public class Almacen
{
    public List<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();

    public void AgregarVehiculo(Vehiculo vehiculo) => Vehiculos.Add(vehiculo);
    public void EliminarVehiculo(long id) => Vehiculos.RemoveAll(v => v.Id == id);
    public Vehiculo BuscarPorPlaca(string placa) => Vehiculos.FirstOrDefault(v => v.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase));
    public bool ConsultarDisponibilidad(long id) => Vehiculos.Any(v => v.Id == id && !v.Vendido);
}
