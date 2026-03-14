public class Moto : Vehiculo
{
     public long Id {get;set;}   
     public string Marca {get;set;} 
     public string Modelo{get;set;}
     public string Color {get;set;} 
     public string Placa {get;set;} 
     public int Cilindraje {get;set;} 
     public decimal Precio {get;set;} 


     public Moto(long id,string marca,string modelo,string color,string placa,int cilindraje,decimal precio):base (id,marca,modelo,color,placa,cilindraje,precio)
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
          if (Cilindraje >= 100 && Cilindraje <= 300)
          {
               iva = 0.10m;
          }
          if (Cilindraje > 300 && Cilindraje <=1000 )
          {
               iva = 0.20m;
          }

          decimal Preciofinal = Precio + iva;
          return Preciofinal;
     }
}