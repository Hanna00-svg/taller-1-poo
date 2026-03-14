using System;
public abstract class Vehiculo : IVendible , IPersistible

    {
        public long Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Placa { get; set; }
        public int Cilindraje { get; set; }
        public decimal Precio { get; set; }
        public bool Vendido { get; set; }

        public Vehiculo(long id, string marca, string modelo, string color, string placa, int cilindraje, decimal precio)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Color = color;
            Placa = placa;
            Cilindraje = cilindraje;
            Precio = precio;
            Vendido = false; 
        }

        public abstract decimal CalcularPrecioFinal();
        public void Vender()
        {
            Vendido = true;
        }

        public virtual string ToCsv()
        {
            return $"{Id},{Marca},{Modelo},{Color},{Placa},{Cilindraje},{Precio},{Vendido}";
        }
    }


