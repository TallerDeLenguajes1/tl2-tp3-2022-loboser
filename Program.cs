using System;
using System.Linq;

namespace tp3
{
    public class Program {
        public static void Main(string[] args){
            Console.Clear();
            int option = 1, id=1, nro=1;
            Cadeteria Cadeteria = CargarCadeteria(id);
            List<Pedidos> ListaPedidosSinAsignar = new List<Pedidos>();
            List<Pedidos> ListaPedidosAsignados = new List<Pedidos>();

            do
            {
            Console.Write("1. Dar alta a pedidos\n2. Asignar pedidos a cadetes\n3. Cambiar de estado los pedidos\n4. Cambiar pedidos de cadetes\n5. Realizar Informe de la jornada\n\nSeleccionar opcion: ");
            int.TryParse(Console.ReadLine(), out option);
            Console.Clear();
                switch (option)
                {
                    case 1:
                        ListaPedidosSinAsignar.Add(AltaPedidos(id, nro));
                        id++;
                        nro++;
                        break;
                    case 2:
                        if (ListaPedidosSinAsignar.Count() > 0 && Cadeteria.ListadoCadetes.Count > 0)
                        {
                            ListaPedidosAsignados.Add(AsignarACadete(ListaPedidosSinAsignar, Cadeteria.ListadoCadetes));
                        }else
                        {
                            Console.WriteLine("No existen cadetes o pedidos sin asignar!");
                        }
                        break;
                    case 3:
                        if (ListaPedidosSinAsignar.Count() > 0 || ListaPedidosAsignados.Count() > 0)
                        {
                            CambiarDeEstado(ListaPedidosSinAsignar, ListaPedidosAsignados);
                        }else
                        {
                            Console.WriteLine("No existen pedidos!");
                        }
                        break;
                    case 4:
                        if (Cadeteria.ListadoCadetes.Count() > 1 && ListaPedidosAsignados.Count > 0)
                        {
                            CambiarDeCadete(Cadeteria.ListadoCadetes);
                        }else
                        {
                            Console.WriteLine("No existe la cantidad suficiente de cadetes para hacer un cambio!");
                        }
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
            Cadeteria.Telefono = "+549381342574";
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
                Console.WriteLine("El Archivo Datos.csv no existe!");
            }
            
            return Cadetes;
        }

        static Pedidos AltaPedidos(int id, int nro){
            Pedidos Pedido = new Pedidos();

            Pedido.Nro = nro;
            Console.Write("Observacion: ");
            Pedido.Obs = Console.ReadLine();     //cargar datos
            Pedido.Cliente = CargarCliente(id);
            Pedido.Estado = "En Proceso";
            nro++;
            Console.Clear();

            return Pedido;
        }

        static Cliente CargarCliente(int id){
            Cliente Cliente = new Cliente();

            Cliente.Id = id;
            Cliente.Nombre = "";                    //ingreso de datos
            Cliente.Direccion = "";
            Cliente.Telefono = "";
            Cliente.DatosReferenciaDireccion = "";
            id++;

            return Cliente;
        }

        static Pedidos AsignarACadete(List<Pedidos> ListaPedidosSinAsignar, List<Cadete> Cadetes){
            int option = 0;

            ListaPedidosSinAsignar
            .ForEach(t => Console.WriteLine($"{t.Nro}. Obs:{t.Obs} Cliente: {t.Cliente.Nombre} Direccion: {t.Cliente.Direccion} Referencia: {t.Cliente.DatosReferenciaDireccion} Estado: {t.Estado}"));

            // seleccionado del pedido a asignar
            do
            {
                Console.Write("Seleccionar pedido a asignar: ");
                int.TryParse(Console.ReadLine(), out option);
            } while (ListaPedidosSinAsignar.Find(t => t.Nro == Convert.ToInt32(option)) == null);
            Console.Clear();
            

            Pedidos Pedido = ListaPedidosSinAsignar
            .First(t => t.Nro == Convert.ToInt32(option));

            //escribir lista de cadetes
            Cadetes
            .ForEach(t => Console.WriteLine($"{t.Id}. Cadete: {t.Nombre}"));

            // seleccionado de cadete para el pedido
            do
            {
                Console.Write("Seleccionar cadete a asignar: ");
                int.TryParse(Console.ReadLine(), out option);
            } while (Cadetes.Find(t => t.Id == Convert.ToInt32(option)) == null);
            Console.Clear();


            Cadetes
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
            do
            {  
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
            } while (i<0 || i> ListaPedidosSinAsignar.Count() + ListaPedidosAsignados.Count()-1);
            Console.Clear();
        }

        static void CambiarDeCadete(List<Cadete> ListadoCadetes){
            int i = 0, j = 1;
            //int[] aux = new int[ListadoCadetes.Where(t => t.ListadoPedidos.Count()!=0).ToList().Count()];             //idea anterior para que solo se pueda seleccionar los cadetes que tenian pedidos

            //escribir cadetes solo con pedidos

            ListadoCadetes.Where(t => t.ListadoPedidos.Count()!=0).ToList()
            .ForEach(t => Console.WriteLine($"ID: {t.Id} Cadete: {t.Nombre}"));

            /*
            foreach (var Cadete in ListadoCadetes.Where(t => t.ListadoPedidos.Count()!=0).ToList())         //Escribia los id de los cadetes con pedidos en un array
            {
                Console.WriteLine($"ID: {Cadete.Id} Cadete: {Cadete.Nombre}");
                aux[i] = Cadete.Id;
                i++;
            }
            */
            
            //seleccionar cadete con i
            do
            {
                Console.Write("Seleccionar el id del cadete con el pedido a cambiar: ");
                int.TryParse(Console.ReadLine(), out i);
            } while (ListadoCadetes.Where(t => t.ListadoPedidos.Count()!=0).ToList().Find(t => t.Id == i) == null);
            //} while (aux.Contains(i)==false);                                                                         //Verificaba si el array contenia el id seleccionado
            Console.Clear();

            //escribe los pedidos de ese cadete
            foreach (var t in ListadoCadetes.First(t => t.Id == i).ListadoPedidos)
            {
                Console.WriteLine($"{j}. Cliente {t.Cliente.Nombre} Estado {t.Estado}");
                j++;
            }

            //seleccionar pedido usando j 
            do
            {
                Console.Write("Seleccionar pedido a cambiar: ");
                int.TryParse(Console.ReadLine(), out j);
            } while (j < 1 || j > ListadoCadetes.First(t => t.Id == i).ListadoPedidos.Count());

            Pedidos Pedido = ListadoCadetes.First(t => t.Id == i).ListadoPedidos[j-1];

            ListadoCadetes.Where(t => t.Id != i).ToList()
            .ForEach(t => Console.WriteLine($"{t.Id}. Cadete {t.Nombre}"));
                //escribir cadetes nuevamente       
            do
            {
                Console.Write("Seleccionar cadete: ");
                int.TryParse(Console.ReadLine(), out j);
            } while (j == i || j<1 || j>ListadoCadetes.Count());  
            Console.Clear();
            
            //seleccionar cadete otra vez reutilizando j
            ListadoCadetes.First(t => t.Id == j).ListadoPedidos.Add(Pedido);
            ListadoCadetes.First(t => t.Id == i).ListadoPedidos.Remove(Pedido);
        }

        static void Informe(List<Cadete> ListadoCadetes){
            double total = 0, monto, cantProm = 0;
            int cant;
            
            foreach (var Cadete in ListadoCadetes)
            {
                cant = Cadete.ListadoPedidos
                .Where(t => t.Estado == "Entregado").ToList().Count();

                monto = cant*300;

                total += monto;
                cantProm += cant;
                Console.WriteLine($"{Cadete.Id}. Cadete: {Cadete.Nombre} Monto ganado: {monto} Cantidad de envios realizados: {cant}");
            }
            Console.WriteLine($"Total: {total} Catidad de envios promedio por cadete: {cantProm/ListadoCadetes.Count()}");
        }
    }
}