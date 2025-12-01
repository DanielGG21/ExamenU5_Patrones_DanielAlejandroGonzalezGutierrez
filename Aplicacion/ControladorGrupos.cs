using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dominio;


namespace Aplicacion
{
    public class ControladorGrupos
    {
        private readonly GrupoDispositivos sala;
        private readonly GrupoDispositivos cocina;
        private readonly GrupoDispositivos cuarto;
        private readonly GrupoDispositivos casa;
        private readonly GrupoDispositivos baño;
        private readonly ControladorEscenasCasa controladorEscenas;

        public ControladorGrupos(
            GrupoDispositivos sala,
            GrupoDispositivos cocina,
            GrupoDispositivos cuarto,
            GrupoDispositivos casa,
            GrupoDispositivos baño,
            ControladorEscenasCasa controladorEscenas)
        {
            this.sala = sala;
            this.cocina = cocina;
            this.cuarto = cuarto;
            this.casa = casa;
            this.baño = baño;
            this.controladorEscenas = controladorEscenas;
        }

        public void Ejecutar()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("====   Control de dispositivos inteligentes   =====================\n");
                Console.WriteLine("Seleccione un grupo:");
                Console.WriteLine("1. Sala");
                Console.WriteLine("2. Cocina");
                Console.WriteLine("3. Cuarto");
                Console.WriteLine("4. Baño");
                Console.WriteLine("6. Casa");
                Console.WriteLine("7. Salir");

                string opcionGrupo = Console.ReadLine();

                GrupoDispositivos grupoSeleccionado = null;

                if (opcionGrupo == "1") grupoSeleccionado = sala;
                else if (opcionGrupo == "2") grupoSeleccionado = cocina;
                else if (opcionGrupo == "3") grupoSeleccionado = cuarto;
                else if (opcionGrupo == "4") grupoSeleccionado = baño;
                else if (opcionGrupo == "6") grupoSeleccionado = casa;
                else if (opcionGrupo == "7")
                {
                    salir = true;
                    Console.WriteLine("Saliendo de control de dispositivos.");
                    Console.ReadKey();
                }

                if (grupoSeleccionado == null)
                {
                    if (!salir)
                    {
                        Console.WriteLine("Opción no válida");
                        Console.ReadKey();
                    }
                    continue;
                }

                if (grupoSeleccionado == casa)
                {
                    MenuCasa();
                }
                else
                {
                    MenuGrupoSimple(grupoSeleccionado);
                }
            }
        }

        private void MenuGrupoSimple(GrupoDispositivos grupo)
        {
            bool regresar = false;

            while (!regresar)
            {
                Console.Clear();
                Console.WriteLine("Seleccione acción para el grupo " + grupo.Nombre + "\n");
                Console.WriteLine("1. Encender todo el grupo");
                Console.WriteLine("2. Apagar todo el grupo");
                Console.WriteLine("3. Encender/Apagar dispositivo específico");
                Console.WriteLine("4. Regresar");

                string opcion = Console.ReadLine();

                var hojasGrupo = grupo.ObtenerDispositivosPlano().ToList();

                if (opcion == "1")
                {
                    bool hayApagados = hojasGrupo.Any(d => !d.EstaEncendido);
                    if (!hayApagados)
                    {
                        Console.Clear();
                        Console.WriteLine("====   Grupo " + grupo.Nombre + "   =====================\n");
                        Console.WriteLine("No hay dispositivos para encender");
                        Console.WriteLine("\n========================================\n");
                        Console.WriteLine("\nPresione una tecla para regresar");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("====   Grupo " + grupo.Nombre + "   =====================");
                        MostrarAnimacion("Encendiendo");
                        grupo.Encender();
                        Console.WriteLine("\n========================================\n");
                        Console.WriteLine("\nPresione una tecla para regresar");
                        Console.ReadKey();
                    }
                }
                else if (opcion == "2")
                {
                    bool hayEncendidos = hojasGrupo.Any(d => d.EstaEncendido);
                    if (!hayEncendidos)
                    {
                        Console.Clear();
                        Console.WriteLine("====   Grupo " + grupo.Nombre + "   =====================\n");
                        Console.WriteLine("No hay dispositivos para apagar");
                        Console.WriteLine("\n========================================\n");
                        Console.WriteLine("\nPresione una tecla para regresar");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("====   Grupo " + grupo.Nombre + "   =====================");
                        MostrarAnimacion("Apagando");
                        grupo.Apagar();
                        Console.WriteLine("\n========================================\n");
                        Console.WriteLine("\nPresione una tecla para regresar");
                        Console.ReadKey();
                    }
                }
                else if (opcion == "3")
                {
                    MenuDispositivosEnGrupo(grupo);
                }
                else if (opcion == "4")
                {
                    regresar = true;
                }
                else
                {
                    Console.WriteLine("Opción no válida");
                    Console.ReadKey();
                }
            }
        }
        private void MenuDispositivosEnGrupo(GrupoDispositivos grupo)
        {
            bool regresar = false;

            while (!regresar)
            {
                var dispositivos = grupo.ObtenerDispositivosPlano().ToList();

                Console.Clear();
                Console.WriteLine("====   Dispositivos en " + grupo.Nombre + "   =====================\n");

                if (dispositivos.Count == 0)
                {
                    Console.WriteLine("No hay dispositivos en este grupo.");
                    Console.WriteLine("\nPresione una tecla para regresar...");
                    Console.ReadKey();
                    return;
                }

                for (int i = 0; i < dispositivos.Count; i++)
                {
                    var d = dispositivos[i];
                    Console.WriteLine(
                        (i + 1) + ". " + d.Nombre + " - " + (d.EstaEncendido ? "Encendido" : "Apagado"));
                }

                Console.WriteLine((dispositivos.Count + 1) + ". Regresar\n");
                Console.Write("Seleccione un dispositivo para alternar su estado (ON/OFF): ");

                string entrada = Console.ReadLine();
                int opcion;

                if (!int.TryParse(entrada, out opcion))
                {
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                    continue;
                }

                if (opcion == dispositivos.Count + 1)
                {
                    regresar = true;
                    continue;
                }

                if (opcion < 1 || opcion > dispositivos.Count)
                {
                    Console.WriteLine("Opción fuera de rango.");
                    Console.ReadKey();
                    continue;
                }

                var seleccionado = dispositivos[opcion - 1];

                Console.Clear();
                Console.WriteLine("====   " + seleccionado.Nombre + "   =====================\n");

                if (seleccionado.EstaEncendido)
                {
                    MostrarAnimacion("Apagando");
                    seleccionado.Apagar();
                }
                else
                {
                    MostrarAnimacion("Encendiendo");
                    seleccionado.Encender();
                }

                Console.WriteLine("\n========================================\n");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        private void MenuCasa()
        {
            bool regresar = false;

            while (!regresar)
            {
                Console.Clear();
                Console.WriteLine("Seleccione acción para el grupo " + casa.Nombre + "\n");
                Console.WriteLine("1. Encender");
                Console.WriteLine("2. Apagar");
                Console.WriteLine("3. Mostrar estado de dispositivos");
                Console.WriteLine("4. Gestionar  guardado dee escenas");
                Console.WriteLine("5. Regresar");

                string opcion = Console.ReadLine();

                var hojasCasa = casa.ObtenerDispositivosPlano().ToList();

                if (opcion == "1")
                {
                    bool hayApagados = hojasCasa.Any(d => !d.EstaEncendido);

                    Console.Clear();
                    Console.WriteLine("====   Grupo " + casa.Nombre + "   =====================");

                    if (!hayApagados)
                    {
                        Console.WriteLine();
                        Console.WriteLine("No hay dispositivos para encender");
                    }
                    else
                    {
                        MostrarAnimacion("Encendiendo");
                        casa.Encender();
                    }

                    Console.WriteLine("\n========================================");
                    Console.WriteLine("\nPresione una tecla para regresar");
                    Console.ReadKey();
                }
                else if (opcion == "2")
                {
                    bool hayEncendidos = hojasCasa.Any(d => d.EstaEncendido);

                    Console.Clear();
                    Console.WriteLine("====   Grupo " + casa.Nombre + "   =====================");

                    if (!hayEncendidos)
                    {
                        Console.WriteLine();
                        Console.WriteLine("No hay dispositivos para apagar");
                    }
                    else
                    {
                        MostrarAnimacion("Apagando");
                        casa.Apagar();
                    }

                    Console.WriteLine("\n========================================");
                    Console.WriteLine("\nPresione una tecla para regresar");
                    Console.ReadKey();
                }
                else if (opcion == "3")
                {
                    Console.Clear();
                    Console.WriteLine("========================================");
                    Console.WriteLine("Estado de dispositivos en " + casa.Nombre + ":");
                    Console.WriteLine("========================================\n");
                    MostrarEstadoPorZonas();
                    Console.WriteLine("\n========================================");
                    Console.WriteLine("\nPresione una tecla para regresar");
                    Console.ReadKey();
                }
                else if (opcion == "4")
                {
                    controladorEscenas.EjecutarMenuEscenas();
                }
                else if (opcion == "5")
                {
                    regresar = true;
                }
                else
                {
                    Console.WriteLine("Opción no válida");
                    Console.ReadKey();
                }
            }
        }

        private void MostrarEstadoPorZonas()
        {
            foreach (var sub in new[] { sala, cocina, cuarto, baño })
            {
                Console.WriteLine();
                Console.WriteLine(sub.Nombre + ":");
                foreach (var d in sub.ObtenerDispositivosPlano())
                {
                    string decor = ObtenerDecoraciones(d);
                    Console.WriteLine("  - " + d.Nombre + " : " + (d.EstaEncendido ? "Encendido" : "Apagado") + decor);
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
                Thread.Sleep(200);
                Console.Write(".");
            }
            int startLeftFull = Math.Max(0, dotsStartLeft - prefix.Length);
            Console.SetCursorPosition(startLeftFull, dotsStartTop);
            Console.Write(new string(' ', prefix.Length + dotCount));
            Console.SetCursorPosition(0, dotsStartTop + 1);
        }

        private static string ObtenerDecoraciones(IDispositivo d)
        {
            var decoradores = new List<string>();
            while (d is DispositivoDecorador dec)
            {
                decoradores.Add(dec.NombreDecorador);
                d = dec.Inner;
            }

            if (decoradores.Count == 0) return string.Empty;

            decoradores.Reverse();
            return " ( " + string.Join(", ", decoradores) + " )";
        }
    }
}
