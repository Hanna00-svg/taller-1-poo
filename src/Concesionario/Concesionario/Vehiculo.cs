
public abstract class Vehiculo : IVendible
{
    public int Id { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Color { get; set; }
    public string Placa { get; set; }
    public decimal Precio { get; set; }
    public int Cilindraje { get; set; }
    public bool Vendido { get; set; }

    public Vehiculo(int id, string marca, string modelo, string color, string placa, decimal precio, int cilindraje, bool vendido = false)
    {
        Id = id;
        Marca = marca;
        Modelo = modelo;
        Color = color;
        Placa = placa;
        Precio = precio;
        Cilindraje = cilindraje;
        Vendido = vendido;
    }

    public abstract decimal CalcularPrecioFinal();
}
