public class Moto : Vehiculo
{
    public Moto(int id, string marca, string modelo, string color, string placa, int cilindraje, decimal precio)
        : base(id, marca, modelo, color, placa, cilindraje, precio)
    {
    }

    public override decimal CalcularPrecioFinal()
    {
        decimal iva = 0;
        if (Cilindraje >= 100 && Cilindraje <= 300)
            iva = 0.10m;
        else if (Cilindraje > 300 && Cilindraje <= 1000)
            iva = 0.20m;

        return Precio + (Precio * iva);
    }

    public override string ToCsv()
    {
        // Prefijo "Moto" para poder reconstruir el tipo correcto al cargar
        return $"Moto,{base.ToCsv()}";
    }
}