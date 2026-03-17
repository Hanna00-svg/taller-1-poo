public class Moto : Vehiculo
{
    public Moto(long id, string marca, string modelo, string color, string placa, decimal precio, int cilindraje, bool vendido = false)
        : base(id, marca, modelo, color, placa, precio, cilindraje, vendido , tipo: "Moto")
    {
    }

    public override decimal CalcularPrecioFinal()
    {
        decimal iva = 0m;

        if (Cilindraje <= 250)
            iva = 0.03m;
        else if (Cilindraje <= 600)
            iva = 0.05m;
        else
            iva = 0.12m;

        return Precio * (1 + iva);
    }
}
