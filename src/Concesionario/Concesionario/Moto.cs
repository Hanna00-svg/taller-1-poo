public class Moto : Vehiculo
{
    public Moto(int id, string marca, string modelo, string color, string placa, decimal precio, int cilindraje, bool vendido = false)
        : base(id, marca, modelo, color, placa, precio, cilindraje, vendido)
    {
    }

    public override decimal CalcularPrecioFinal()
    {
        decimal iva = 0m;

        if (Cilindraje > 250 && Cilindraje <= 600)
            iva = 0.05m; // ejemplo: 5% IVA
        else if (Cilindraje > 600)
            iva = 0.12m; // ejemplo: 12% IVA

        return Precio * (1 + iva);
    }
}
