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


        private List<IDispositivo> dispositivos;
        private ConstructorCasa constructor;
        private EstadoCasaOriginator originator;
        private HistorialEscenasCasa historial;
        private ControladorEscenasCasa controladorEscenas;
        private ControladorGrupos controladorGrupos;

        private CentroDeControlCasa() { }


    

        public void Iniciar()
        {
            dispositivos = CrearDispositivosIniciales();

            var servicioDecoracion = new ServicioDecoracionDispositivos();
            servicioDecoracion.DecorarDispositivos(dispositivos);

            constructor = new ConstructorCasa(dispositivos);

            originator = new EstadoCasaOriginator(constructor.Casa);
            historial = new HistorialEscenasCasa();

            controladorEscenas = new ControladorEscenasCasa(originator, historial);

            controladorGrupos = new ControladorGrupos(
                constructor.Sala,
                constructor.Cocina,
                constructor.Cuarto,
                constructor.Casa,
                constructor.Baño,
                controladorEscenas
            );

            controladorGrupos.Ejecutar(); 
        }

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
