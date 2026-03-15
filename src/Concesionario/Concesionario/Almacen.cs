using System.Data.Common;

public class Almacen
    {
        public List<Vehiculo> Vehiculos { get; set; }
        private string rutaArchivo = "vehiculos.csv";

        public Almacen()
        {
            Vehiculos = new List<Vehiculo>();
        }

        // Agregar vehículo
        public void AgregarVehiculo(Vehiculo vehiculo)
        {
            Vehiculos.Add(vehiculo);
        }

        // Eliminar vehículo por Id
        public void EliminarVehiculo(long id)
        {
            Vehiculo vehiculoEliminar = Vehiculos.Find(v => v.Id == id);
            if (vehiculoEliminar != null)
            {
                Vehiculos.Remove(vehiculoEliminar);
                Console.WriteLine($"Vehículo con ID {id} ha sido eliminado.");
            }
            else
            {
                Console.WriteLine($"No se encontró ningún vehículo con el ID {id}.");
            }
        }

        // Consultar disponibilidad por Id
        public bool ConsultarDisponibilidad(long id)
        {
            return Vehiculos.Any(v => v.Id == id);
        }

        // Buscar vehículo por placa
        public Vehiculo BuscarPorPlaca(string placa)
        {
            return Vehiculos.FirstOrDefault(v => v.Placa == placa);
        }

        // Guardar vehículos en archivo CSV usando IPersistible
        public void GuardarVehiculos()
        {
            using (StreamWriter writer = new StreamWriter(rutaArchivo))
            {
                writer.WriteLine("Id,Marca,Modelo,Color,Placa,Cilindraje,Precio,Vendido,Tipo");

                foreach (var v in Vehiculos)
                {
                    writer.WriteLine(v.ToCsv()); // 👈 delega en la interfaz
                }
            }
            Console.WriteLine("Vehículos guardados en archivo CSV.");
        }

        // Cargar vehículos desde archivo CSV
        public void CargarVehiculos()
        {
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("No se encontró el archivo de datos.");
                return;
            }

            using (StreamReader reader = new StreamReader(rutaArchivo))
            {
                reader.ReadLine(); // saltar encabezado
                Vehiculos.Clear();

                while (!reader.EndOfStream)
                {
                    string linea = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    string[] datos = linea.Split(',');

                    int id = int.Parse(datos[0]);
                    string marca = datos[1];
                    string modelo = datos[2];
                    string color = datos[3];
                    string placa = datos[4];
                    int cilindraje = int.Parse(datos[5]);
                    decimal precio = decimal.Parse(datos[6]);
                    bool vendido = bool.Parse(datos[7]);
                    string tipo = datos[8];

                    Vehiculo v = tipo == "Carro"
                        ? new Carro(id, marca, modelo, color, placa, cilindraje, precio)
                        : new Moto(id, marca, modelo, color, placa, cilindraje, precio);

                    if (vendido) v.Vender();
                    Vehiculos.Add(v);
                }
            }

            Console.WriteLine($"Se cargaron {Vehiculos.Count} vehículos exitosamente.");
        }
    }
