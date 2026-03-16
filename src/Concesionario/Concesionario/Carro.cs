public class Carro : Vehiculo
{
    public Carro(int id, string marca, string modelo, string color, string placa, int cilindraje, decimal precio)
        : base(id, marca, modelo, color, placa, cilindraje, precio)
    {
    }

    public override decimal CalcularPrecioFinal()
    {
        decimal iva = 0;
        if (Cilindraje >= 1400 && Cilindraje <= 2000)
            iva = 0.10m;
        else if (Cilindraje > 2000 && Cilindraje <= 2500)
            iva = 0.20m;

        return Precio + (Precio * iva);
    }

    public override string ToCsv()
    {
        // Prefijo "Carro" para poder reconstruir el tipo correcto al cargar
        return $"Carro,{base.ToCsv()}";
    }
}
