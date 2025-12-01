using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;                


namespace Aplicacion
{
    public sealed class CentroDeControlCasa
    {
        // ─────────────────────────────────────────────
        //       SINGLETON CLÁSICO (DOBLE VERIFICACIÓN)
        // ─────────────────────────────────────────────

        private static CentroDeControlCasa _instancia = null;
        private static readonly object _lock = new object();

        public static CentroDeControlCasa Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    lock (_lock)
                    {
                        if (_instancia == null)
                        {
                            _instancia = new CentroDeControlCasa();
                        }
                    }
                }

                return _instancia;
            }
        }

        // ─────────────────────────────────────────────
        //              CAMPOS INTERNOS
        // ─────────────────────────────────────────────

        private List<IDispositivo> dispositivos;
        private ConstructorCasa constructor;
        private EstadoCasaOriginator originator;
        private HistorialEscenasCasa historial;
        private ControladorEscenasCasa controladorEscenas;
        private ControladorGrupos controladorGrupos;

        // Constructor privado → obligatorio del Singleton
        private CentroDeControlCasa() { }


        // ─────────────────────────────────────────────
        //              MÉTODO PRINCIPAL
        // ─────────────────────────────────────────────

        public void Iniciar()
        {
            // 1. Crear dispositivos de forma inicial
            dispositivos = CrearDispositivosIniciales();

            // 2. Decoración (mismo flujo que antes)
            var servicioDecoracion = new ServicioDecoracionDispositivos();
            servicioDecoracion.DecorarDispositivos(dispositivos);

            // 3. Crear estructura de la casa (Composite)
            constructor = new ConstructorCasa(dispositivos);

            // 4. Crear Originator + Historial (Memento)
            originator = new EstadoCasaOriginator(constructor.Casa);
            historial = new HistorialEscenasCasa();

            // 5. Controlador de escenas (Aplicación)
            controladorEscenas = new ControladorEscenasCasa(originator, historial);

            // 6. Controlador principal de grupos
            controladorGrupos = new ControladorGrupos(
                constructor.Sala,
                constructor.Cocina,
                constructor.Cuarto,
                constructor.Casa,
                constructor.Baño,
                controladorEscenas
            );

            // 7. Ejecutar menú principal
            controladorGrupos.Ejecutar();
        }


        // ─────────────────────────────────────────────
        //              CREAR DISPOSITIVOS
        // ─────────────────────────────────────────────

        private List<IDispositivo> CrearDispositivosIniciales()
        {
            return new List<IDispositivo>
            {
                new DispositivoSimple("Televisor"),
                new DispositivoSimple("Bocinas"),
                new DispositivoSimple("Foco sala"),
                new DispositivoSimple("Microondas"),
                new DispositivoSimple("Refrigerador"),
                new DispositivoSimple("Luces LED cuarto"),
                new DispositivoSimple("PC escritorio"),
                new DispositivoSimple("Foco Sanitario")
            };
        }
    }
}
