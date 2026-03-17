public class Carro : Vehiculo
{
    public Carro(long id, string marca, string modelo, string color, string placa, decimal precio, int cilindraje, bool vendido = false)
        : base(id, marca, modelo, color, placa, precio, cilindraje, vendido , tipo: "Carro")
    {
    }

    public override decimal CalcularPrecioFinal()
    {
        decimal iva;

        if (Cilindraje < 1400)
            iva = 0.05m;
        else if (Cilindraje <= 2000)
            iva = 0.10m;
        else if (Cilindraje <= 2500)
            iva = 0.20m;
        else
            iva = 0.30m;

    return Precio * (1 + iva);
    }
}

