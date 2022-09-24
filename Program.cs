using System;
using System.Linq;

namespace tp3
{
    
    public class Program {
        public static void Main(string[] args){
            int option = 1, id=1, nro=1;
            Cadeteria Cadeteria = CargarCadeteria(id);
            List<Pedidos> ListaPedidosSinAsignar = new List<Pedidos>();
            List<Pedidos> ListaPedidosAsignados = new List<Pedidos>();

            do
            {
            Console.Write("Seleccionar opcion: ");
            option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        ListaPedidosSinAsignar.Add(AltaPedidos(id, nro));
                        id++;
                        nro++;
                        break;
                    case 2:
                        ListaPedidosAsignados.Add(AsignarACadete(ListaPedidosSinAsignar, Cadeteria));
                        break;
                    case 3:
                        CambiarDeEstado(ListaPedidosSinAsignar, ListaPedidosAsignados);
                        break;
                    case 4:
                        CambiarDeCadete(Cadeteria.ListadoCadetes);
                        break;
                    case 5:
                        Informe(Cadeteria.ListadoCadetes);
                        break;
                    default:
                        break;
                }
            } while (option!=5);
        }

        static Cadeteria CargarCadeteria(int id){
            Cadeteria Cadeteria = new Cadeteria();

            Cadeteria.Nombre = "Cadeteria Of";
            Cadeteria.Telefono = "+5493816249326";
            Cadeteria.ListadoCadetes = new List<Cadete>();
 
            Cadeteria.ListadoCadetes = CargarCadetes(id);

            return Cadeteria;
        }

        static List<Cadete> CargarCadetes(int id){
            List<Cadete> Cadetes = new List<Cadete>();

            if(File.Exists("Datos.csv") && new FileInfo("Datos.csv").Length > 0){
                string[] lineas = File.ReadAllLines("Datos.csv");

                foreach (var linea in lineas)
                {
                    Cadete Cadete = new Cadete();

                    string[] line = linea.Split(';');

                    Cadete.Id = id;
                    Cadete.Nombre = line[0];
                    Cadete.Direccion = line[1];
                    Cadete.Telefono = line[2];
                    Cadete.ListadoPedidos = new List<Pedidos>();
                    id++;

                    Cadetes.Add(Cadete);
                }
            }else
            {
                
            }
            
            return Cadetes;
        }

        static Pedidos AltaPedidos(int id, int nro){
            Pedidos Pedido = new Pedidos();

            Pedido.Nro = nro;
            Console.Write("Observacion: ");
            Pedido.Obs = Console.ReadLine().ToString();
            Pedido.Cliente = CargarCliente(id);
            Pedido.Estado = "En Proceso";
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

        static Pedidos AsignarACadete(List<Pedidos> ListaPedidosSinAsignar, Cadeteria Cadeteria){
            int option = 0;

            ListaPedidosSinAsignar
            .ForEach(t => Console.WriteLine($"{t.Nro}. Cliente: {t.Cliente.Nombre} Direccion: {t.Cliente.Direccion} Referencia: {t.Cliente.DatosReferenciaDireccion} Estado: {t.Estado}"));

            // seleccionado del pedido a asignar
            Console.Write("Seleccionar pedido a asignar: ");
            option = Convert.ToInt32(Console.ReadLine());

            Pedidos Pedido = ListaPedidosSinAsignar
            .First(t => t.Nro == Convert.ToInt32(option));

            //escribir lista de cadetes
            Cadeteria.ListadoCadetes
            .ForEach(t => Console.WriteLine($"{t.Id}. Cadete: {t.Nombre}"));

            // seleccionado de cadete para el pedido
            Console.Write("Seleccionar cadete a asignar: ");
            option = Convert.ToInt32(Console.ReadLine());

            Cadeteria.ListadoCadetes
            .First(t => t.Id == Convert.ToInt32(option)).ListadoPedidos.Add(Pedido);
            ListaPedidosSinAsignar.Remove(Pedido);

            return Pedido;
        }

        static void CambiarDeEstado(List<Pedidos> ListaPedidosSinAsignar, List<Pedidos> ListaPedidosAsignados){
            int i = 0;

            foreach (var t in ListaPedidosSinAsignar)
            {
                i++;
                Console.WriteLine($"{i}. Cliente: {t.Cliente.Nombre} Estado: {t.Estado}");
            }

            foreach (var t in ListaPedidosAsignados)
            {
                i++;
                Console.WriteLine($"{i}. Cliente: {t.Cliente.Nombre} Estado: {t.Estado}");
            }

            // seleccionar el pedido a cambiar el estado
            Console.Write("Seleccionar pedido a cambiar de estado: ");
            i = Convert.ToInt32(Console.ReadLine())-1;

            if(i <= ListaPedidosSinAsignar.Count()-1){
                if (ListaPedidosSinAsignar[i].Estado == "Entregado")
                {
                    ListaPedidosSinAsignar[i].Estado = "En Proceso";
                }else
                {
                    ListaPedidosSinAsignar[i].Estado = "Entregado";
                }
                //cambiar el estado
            }else if(i >= ListaPedidosSinAsignar.Count())
            {
                if (ListaPedidosAsignados[i-ListaPedidosSinAsignar.Count()].Estado == "Entregado")
                {
                    ListaPedidosAsignados[i-ListaPedidosSinAsignar.Count()].Estado = "En Proceso";
                }else
                {
                    ListaPedidosAsignados[i-ListaPedidosSinAsignar.Count()].Estado = "Entregado";
                }
            }
        }

        static void CambiarDeCadete(List<Cadete> ListadoCadetes){
            int i = 0, j = 1;
            int[] aux = new int[ListadoCadetes.Count()];
            //escribir cadetes solo con pedidos
            foreach (var Cadete in ListadoCadetes.Where(t => t.ListadoPedidos.Count()!=0).ToList())
            {
                Console.WriteLine($"ID: {Cadete.Id} Cadete: {Cadete.Nombre}");
                aux[i] = Cadete.Id;
                i++;
            }
            
            //seleccionar cadete con i
            do
            {
            Console.Write("Seleccionar el id del cadete con el pedido a cambiar: ");
            i = Convert.ToInt32(Console.ReadLine());
            } while (aux.Contains(i)==false);
            //escribe los pedidos de ese cadete
            foreach (var t in ListadoCadetes.First(t => t.Id == i).ListadoPedidos)
            {
                Console.WriteLine($"{j}. Cliente {t.Cliente.Nombre} Estado {t.Estado}");
                j++;
            }

            //seleccionar pedido usando j 
            Console.Write("Seleccionar pedido a cambiar: ");
            j = Convert.ToInt32(Console.ReadLine())-1;

            Pedidos Pedido = ListadoCadetes.First(t => t.Id == i).ListadoPedidos[j];

            ListadoCadetes.Where(t => t.Id != i).ToList()
            .ForEach(t => Console.WriteLine($"{t.Id}. Cadete {t.Nombre}"));
                //escribir cadetes nuevamente       
            do
            {
                Console.Write("Seleccionar cadete: ");
                j = Convert.ToInt32(Console.ReadLine());
            } while (j == i || j<0 || j>ListadoCadetes.Count());  
            
            //seleccionar cadete otra vez reutilizando j
            ListadoCadetes[j].ListadoPedidos.Add(Pedido);
            ListadoCadetes.First(t => t.Id == i).ListadoPedidos.Remove(Pedido);
        }

        static void Informe(List<Cadete> ListadoCadetes){
            double total = 0, monto, cantProm = 0;
            int cant;
            
            foreach (var Cadete in ListadoCadetes)
            {
                monto = 0;
                cant = 0;
                foreach (var Pedidos in Cadete.ListadoPedidos)
                {
                    if (Pedidos.Estado == "Entregado")
                    {
                        monto += 300;
                        cant++;
                    }
                }
                total += monto;
                cantProm += cant;
                Console.WriteLine($"{Cadete.Id}. Cadete: {Cadete.Nombre} Monto ganado: {monto} Cantidad de envios realizados: {cant}");
            }
            Console.WriteLine($"Total: {total} Catidad de envios promedio por cadete: {cantProm/ListadoCadetes.Count()}");
        }
    }
}