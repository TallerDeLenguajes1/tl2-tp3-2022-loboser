namespace tp3
{
    public class Persona {
        private int id;
        private string telefono;
        private string nombre;
        private string direccion;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
    }

    public class Cliente : Persona {
        string datosReferenciaDireccion;

        public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }
    }

    public class Pedidos {
        private int nro;
        private string obs;
        private Cliente cliente;
        private bool estado;

        public int Nro { get => nro; set => nro = value; }
        public string Obs { get => obs; set => obs = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public bool Estado { get => estado; set => estado = value; }
    }

    public class Cadete : Persona {
        private List<Pedidos> listadoPedidos = new List<Pedidos>();

        public List<Pedidos> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

        int JornalACobrar(){
            return listadoPedidos.Count()*300;
        }
    }

    public class Cadeteria {
        private string nombre;
        private string telefono;
        private List<Cadete> listadoCadetes = new List<Cadete>();

        public string Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    }
}