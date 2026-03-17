public interface IVendible
{
    public bool Vendido { get; set; }
    public decimal CalcularPrecioFinal();

}
