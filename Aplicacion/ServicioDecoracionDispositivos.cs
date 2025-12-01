using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Aplicacion
{
    public class ServicioDecoracionDispositivos
    {
        public void DecorarDispositivos(List<IDispositivo> dispositivos)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("====   Dispositivos disponibles para decorar:   =====================\n");

                for (int i = 0; i < dispositivos.Count; i++)
                {
                    bool esDecorado = dispositivos[i] is DispositivoDecorador;
                    string etiqueta = esDecorado ? " (decorado)" : "";
                    Console.WriteLine((i + 1) + ". " + dispositivos[i].Nombre + etiqueta);
                }

                Console.WriteLine((dispositivos.Count + 1) + ". Terminar decoracion \n");

                Console.WriteLine("Seleccione una opción:");
                string entrada = Console.ReadLine();
                int opcion;

                if (!int.TryParse(entrada, out opcion))
                {
                    Console.WriteLine("Opción inválida");
                    Console.ReadKey();
                    continue;
                }

                if (opcion == dispositivos.Count + 1)
                {
                    Console.WriteLine("Decoracion finalizada presione una tecla para continuar");
                    Console.ReadKey();
                    break;
                }

                if (opcion < 1 || opcion > dispositivos.Count)
                {
                    Console.WriteLine("Opción inválida");
                    Console.ReadKey();
                    continue;
                }

                IDispositivo seleccionado = dispositivos[opcion - 1];

                var decoradoresAplicados = ObtenerDecoradoresAplicados(seleccionado);

                while (true)
                {
                    bool esParaModoCine =
                        seleccionado.Nombre.Contains("Televisor") ||
                        seleccionado.Nombre.Contains("Bocinas") ||
                        seleccionado.Nombre.Contains("Foco sala");

                    int maxTiposPosibles = esParaModoCine ? 3 : 2;

                    if (decoradoresAplicados.Count >= maxTiposPosibles)
                    {
                        Console.WriteLine("\nEste dispositivo ya tiene todos los decoradores disponibles.");
                        Console.WriteLine("Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                    }

                    int tipoDecorador = ElegirTipoDecorador(seleccionado.Nombre, decoradoresAplicados);

                    if (tipoDecorador == 0)
                    {
                        Console.WriteLine("\nDecoración finalizada para " + seleccionado.Nombre + ".");
                        Console.WriteLine("Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                    }

                    seleccionado = AplicarDecorador(seleccionado, tipoDecorador);

                    decoradoresAplicados.Add(ObtenerNombreDecoradorPorTipo(tipoDecorador));

                    dispositivos[opcion - 1] = seleccionado;
                }
            }
        }


        private int ElegirTipoDecorador(string nombreDispositivo, List<string> decoradoresAplicados)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.WriteLine("Seleccione decorador para " + nombreDispositivo);
                Console.WriteLine("========================================");
                Console.WriteLine("Elija decorador:\n");

                bool esParaModoCine =
                    nombreDispositivo.Contains("Televisor") ||
                    nombreDispositivo.Contains("Bocinas") ||
                    nombreDispositivo.Contains("Foco sala");

                bool tieneAhorro = decoradoresAplicados.Contains("Ahorro de energía");
                bool tieneNocturno = decoradoresAplicados.Contains("Modo nocturno");
                bool tieneCine = decoradoresAplicados.Contains("Modo Cine");

                if (!tieneAhorro)
                    Console.WriteLine("1. Decorador ahorro de energía");

                if (!tieneNocturno)
                    Console.WriteLine("2. Decorador modo nocturno");

                if (!tieneCine && esParaModoCine)
                    Console.WriteLine("3. Decorador modo cine");

                Console.WriteLine("4. Terminar decoración");

                string opcion = Console.ReadLine();

                if (opcion == "1" && !tieneAhorro) return 1;
                if (opcion == "2" && !tieneNocturno) return 2;
                if (opcion == "3" && esParaModoCine && !tieneCine) return 3;
                if (opcion == "4") return 0;

                Console.WriteLine("Opción no válida, intente de nuevo.");
                Console.ReadKey();
            }
        }

        private IDispositivo AplicarDecorador(IDispositivo baseDispositivo, int tipo)
        {
            if (tipo == 1) return new DecoradorAhorroEnergia(baseDispositivo);
            if (tipo == 2) return new DecoradorModoNocturno(baseDispositivo);
            if (tipo == 3) return new DecoradorModoCine(baseDispositivo);
            return baseDispositivo;
        }

        private List<string> ObtenerDecoradoresAplicados(IDispositivo dispositivo)
        {
            var decoradores = new List<string>();
            IDispositivo actual = dispositivo;

            while (actual is DispositivoDecorador dec)
            {
                decoradores.Add(dec.NombreDecorador);  
                actual = dec.Inner;
            }

            return decoradores;
        }

        private string ObtenerNombreDecoradorPorTipo(int tipo)
        {
            if (tipo == 1) return "Ahorro de energía";
            if (tipo == 2) return "Modo nocturno";
            if (tipo == 3) return "Modo Cine";
            return "";
        }
    }
}
