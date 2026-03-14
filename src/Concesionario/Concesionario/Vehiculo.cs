using System;

namespace Concesionario
{
    public abstract class Vehiculo : IVendible, IPersistible
    {

        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Color { get; set; }
        public string Placa { get; set; }
        public decimal Precio { get; set; }


        public Vehiculo(int id, string marca, string modelo, string color, string placa, decimal precio)
        {
            Id = id;
            Marca = marca;
            Modelo = modelo;
            Color = color;
            Placa = placa;
            Precio = precio;
        }

        public abstract decimal CalcularPrecioFinal();

        public virtual string ToCsv()
        {
            return $"{Id},{Marca},{Modelo},{Color},{Placa},{Precio}";
        }
    }
}
