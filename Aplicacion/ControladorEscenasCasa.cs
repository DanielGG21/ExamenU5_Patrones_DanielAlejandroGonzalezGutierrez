using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;                 

namespace Aplicacion
{
    public class ControladorEscenasCasa
    {
        private readonly EstadoCasaOriginator originatorCasa;
        private readonly HistorialEscenasCasa historialEscenas;

        public ControladorEscenasCasa(EstadoCasaOriginator originatorCasa, HistorialEscenasCasa historialEscenas)
        {
            this.originatorCasa = originatorCasa;
            this.historialEscenas = historialEscenas;
        }

        public void EjecutarMenuEscenas()
        {
            bool regresar = false;

            while (!regresar)
            {
                Console.Clear();
                Console.WriteLine("====   Gestor de Guardado de casa    =====================\n");
                Console.WriteLine("1. Guardar configuración actual como nueva escena");
                Console.WriteLine("2. Listar escenas guardadas");
                Console.WriteLine("3. Restaurar una escena");
                Console.WriteLine("4. Eliminar una escena");
                Console.WriteLine("5. Limpiar todas las escenas");
                Console.WriteLine("6. Regresar");

                string opcion = Console.ReadLine();

                if (opcion == "1")
                {
                    Console.Write("Nombre para la nueva escena: ");
                    string nombre = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(nombre))
                    {
                        nombre = "Escena " + (historialEscenas.Escenas.Count + 1);
                    }

                    var memento = originatorCasa.CrearMemento(nombre);
                    historialEscenas.AgregarEscena(memento);

                    Console.WriteLine("\nEscena guardada correctamente.");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                }
                else if (opcion == "2")
                {
                    Console.Clear();
                    Console.WriteLine("====   Escenas guardadas   =====================\n");

                    if (!historialEscenas.TieneEscenas)
                    {
                        Console.WriteLine("No hay escenas guardadas.");
                    }
                    else
                    {
                        for (int i = 0; i < historialEscenas.Escenas.Count; i++)
                        {
                            var e = historialEscenas.Escenas[i];
                            Console.WriteLine((i + 1) + ". " + e.Nombre + " - " + e.FechaCreacion);
                        }
                    }

                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }
                else if (opcion == "3")
                {
                    if (!historialEscenas.TieneEscenas)
                    {
                        Console.WriteLine("\nNo hay escenas para restaurar.");
                        Console.WriteLine("Presione una tecla para continuar...");
                        Console.ReadKey();
                        continue;
                    }

                    Console.Clear();
                    Console.WriteLine("====   Restaurar escena   =====================\n");
                    for (int i = 0; i < historialEscenas.Escenas.Count; i++)
                    {
                        var e = historialEscenas.Escenas[i];
                        Console.WriteLine((i + 1) + ". " + e.Nombre + " - " + e.FechaCreacion);
                    }

                    Console.Write("\nSeleccione el número de la escena a restaurar: ");
                    string entrada = Console.ReadLine();
                    if (int.TryParse(entrada, out int indice))
                    {
                        indice -= 1;
                        var escena = historialEscenas.ObtenerEscena(indice);
                        if (escena != null)
                        {
                            Console.Clear();
                            Console.WriteLine("Restaurando escena: " + escena.Nombre);
                            MostrarAnimacion("Restaurando");
                            originatorCasa.RestaurarDesdeMemento(escena);
                            Console.WriteLine("\nEscena restaurada correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("Índice de escena inválido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida.");
                    }

                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }
                else if (opcion == "4")
                {
                    if (!historialEscenas.TieneEscenas)
                    {
                        Console.WriteLine("\nNo hay escenas para eliminar.");
                        Console.WriteLine("Presione una tecla para continuar...");
                        Console.ReadKey();
                        continue;
                    }

                    Console.Clear();
                    Console.WriteLine("====   Eliminar escena   ======================\n");
                    for (int i = 0; i < historialEscenas.Escenas.Count; i++)
                    {
                        var e = historialEscenas.Escenas[i];
                        Console.WriteLine((i + 1) + ". " + e.Nombre + " - " + e.FechaCreacion);
                    }

                    Console.Write("\nSeleccione el número de la escena a eliminar: ");
                    string entrada = Console.ReadLine();
                    if (int.TryParse(entrada, out int indice))
                    {
                        indice -= 1;
                        var escena = historialEscenas.ObtenerEscena(indice);
                        if (escena != null)
                        {
                            historialEscenas.EliminarEscena(indice);
                            Console.WriteLine("\nEscena eliminada correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("Índice de escena inválido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida.");
                    }

                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }
                else if (opcion == "5")
                {
                    if (!historialEscenas.TieneEscenas)
                    {
                        Console.WriteLine("\nNo hay escenas que limpiar.");
                    }
                    else
                    {
                        historialEscenas.Limpiar();
                        Console.WriteLine("\nTodas las escenas han sido eliminadas.");
                    }

                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                }
                else if (opcion == "6")
                {
                    regresar = true;
                }
                else
                {
                    Console.WriteLine("Opción no válida.");
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private static void MostrarAnimacion(string texto)
        {
            Console.Write("Cargando");
            int dotCount = 10;
            int dotsStartLeft = Console.CursorLeft;
            int dotsStartTop = Console.CursorTop;
            string prefix = "Cargando";

            for (int i = 0; i < dotCount; i++)
            {
                System.Threading.Thread.Sleep(200);
                Console.Write(".");
            }
            int startLeftFull = Math.Max(0, dotsStartLeft - prefix.Length);
            Console.SetCursorPosition(startLeftFull, dotsStartTop);
            Console.Write(new string(' ', prefix.Length + dotCount));
            Console.SetCursorPosition(0, dotsStartTop + 1);
        }
    }
}
