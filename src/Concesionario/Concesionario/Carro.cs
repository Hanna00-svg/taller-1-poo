public class Carro : Vehiculo
{
    public int Id {get;set;}   
    public string Marca {get;set;} 
    public string Modelo{get;set;}
    public string Color {get;set;} 
    public string Placa {get;set;} 
    public int Cilindraje {get;set;} 
    public decimal Precio {get;set;} 
    

    public Carro(int id,string marca, string modelo,string color,string placa,int cilindraje,decimal precio): base (id,marca,modelo,color,placa,cilindraje,precio)
    {
        Id = id;
        Marca = marca;
        Color = color;
        Placa = placa;
        Cilindraje = cilindraje;
        Precio = precio;
        
    }

    public override decimal CalcularPrecioFinal()
    {
        decimal iva = 0;
        if (Cilindraje >= 1400 && Cilindraje <= 2000)
        {
            iva = 0.10m;
        }
        if (Cilindraje > 2000 && Cilindraje <= 2500)
        {
            iva = 0.20m;
        }

        decimal Preciofinal = Precio + iva;
        return Preciofinal;
    }
}
