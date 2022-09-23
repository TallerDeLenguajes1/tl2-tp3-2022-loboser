using System;

namespace tp3
{
    
    public class Program {
        public static void Main(string[] args){
            int option = 1, id=1, nro=1;
            Cadeteria Cadeteria = CargarCadeteria(id);
            List<Pedidos> ListaPedidosSinAsignar = new List<Pedidos>();
            List<Pedidos> ListaPedidosAsignados = new List<Pedidos>();

            
            switch (option)
            {
                case 1:
                break;
                case 2:
                break;
                case 3:
                    ListaPedidosSinAsignar.Add(AltaPedidos(id, nro));
                break;
                case 4:
                break;
                default:
                break;
            }
        }

        static Cadeteria CargarCadeteria(int id){
            Cadeteria Cadeteria = new Cadeteria();

            Cadeteria.Nombre = "";
            Cadeteria.Telefono = "";
            Cadeteria.ListadoCadetes = new List<Cadete>();
            int cantCadetes = 5;
            for (int i = 0; i < cantCadetes; i++)
            {
                Cadeteria.ListadoCadetes.Add(CargarCadete(id));
            }
            return Cadeteria;
        }

        static Cadete CargarCadete(int id){
            Cadete Cadete = new Cadete();

            Cadete.Id = id;
            Cadete.Nombre = "";
            Cadete.Direccion = "";
            Cadete.Telefono = "";
            Cadete.ListadoPedidos = new List<Pedidos>();
            id++;
            
            return Cadete;
        }

        static Pedidos AltaPedidos(int id, int nro){
            Pedidos Pedido = new Pedidos();

            Pedido.Nro = nro;
            Pedido.Obs = "";
            Pedido.Cliente = CargarCliente(id);
            Pedido.Estado = true;
            nro++;

            return Pedido;
        }

        static Cliente CargarCliente(int id){
            Cliente Cliente = new Cliente();

            Cliente.Id = id;
            Cliente.Nombre = "";
            Cliente.Direccion = "";
            Cliente.Telefono = "";
            Cliente.DatosReferenciaDireccion = "";
            id++;

            return Cliente;
        }

        static Pedidos asignarACadete(List<Pedidos> ListaPedidosSinAsignar, Cadeteria Cadeteria){
            int i=0, j=0;

            foreach (var PedidoSinAsignar in ListaPedidosSinAsignar)
            {
                //escribir lista de pedidos
            }
            // seleccionado del pedido a asignar

            Pedidos Pedido = ListaPedidosSinAsignar[i];
            ListaPedidosSinAsignar.Remove(Pedido);

            foreach (var Cadete in Cadeteria.ListadoCadetes)
            {
                //escribir lista de cadetes
            }

            // seleccionado de cadete para el pedido
            
            Cadeteria.ListadoCadetes[j].ListadoPedidos.Add(Pedido);
            return Pedido;
        }

        static void CambiarDeEstado(List<Pedidos> ListaPedidosSinAsignar, List<Pedidos> ListaPedidosAsignados){
            int i=1, aux = 1;
            foreach (var PedidoSinAsignar in ListaPedidosSinAsignar)
            {

                //contar la cantidad
                aux++;
            }

            foreach (var PedidoAsignado in ListaPedidosAsignados)
            {
                
            }

            // seleccionar el pedido a cambiar el estado


            if(i<=aux){
                ListaPedidosSinAsignar[i].Estado = true;        //cambiar el estado
            }else
            {
                ListaPedidosAsignados[i].Estado = true;
            }
        }

        static void CambiarDeCadete(Cadeteria Cadeteria){
            int i = 0, j = 0;
            foreach (var Cadete in Cadeteria.ListadoCadetes)
            {
                //escribir cadetes
            }

            //seleccionar cadete con i

            foreach (var pedido in Cadeteria.ListadoCadetes[i].ListadoPedidos)
            {
                //escribir pedidos
            }

            //seleccionar pedidos usando j 
            Pedidos Pedido = Cadeteria.ListadoCadetes[i].ListadoPedidos[j];

            foreach (var Cadete in Cadeteria.ListadoCadetes)
            {
                //escribir cadetes nuevamente                
            }
            
            //seleccionar cadete otra vez reutilizando j

            Cadeteria.ListadoCadetes[j].ListadoPedidos.Add(Pedido);
            Cadeteria.ListadoCadetes[i].ListadoPedidos.Remove(Pedido);
        }

        static void Informe(Cadeteria Cadeteria){
            //escribir informe mediante linq
        }
    }
}