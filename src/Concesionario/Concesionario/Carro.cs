public class Carro : Vehiculo
{
    public Carro(long id, string marca, string modelo, string color, string placa, decimal precio, int cilindraje, bool vendido = false)
        : base(id, marca, modelo, color, placa, precio, cilindraje, vendido)
    {
    }

    public override decimal CalcularPrecioFinal()
    {
        decimal iva = 0m;

        if (Cilindraje >= 1400 && Cilindraje <= 2000)
            iva = 0.10m;
        else if (Cilindraje > 2000 && Cilindraje <= 2500)
            iva = 0.20m;

        return Precio * (1 + iva);
    }
}

