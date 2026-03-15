public class Cliente
{

    public int Cedula{get; set;}
    public string Nombre{get; set;}
    public string Telefono{get; set;}
    public string Direccion{get; set;}

    public Cliente(int cedula, string nombre, string telefono, string direccion)
    {

        Cedula = cedula;
        Nombre = nombre;
        Telefono = telefono;
        Direccion = direccion;

    }

}