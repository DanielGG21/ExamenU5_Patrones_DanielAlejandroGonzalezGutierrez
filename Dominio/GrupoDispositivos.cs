using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class GrupoDispositivos : IDispositivo
    {
        private readonly List<IDispositivo> dispositivos = new List<IDispositivo>();

        public string Nombre { get; }

        public GrupoDispositivos(string nombre)
        {
            Nombre = nombre;
        }

        public bool EstaEncendido
        {
            get
            {
                foreach (var d in dispositivos)
                {
                    if (d.EstaEncendido) return true;
                }
                return false;
            }
        }

        public void AgregarDispositivo(IDispositivo dispositivo)
        {
            dispositivos.Add(dispositivo);
        }

        public void Encender()
        {
            Console.WriteLine();
            Console.WriteLine("Encendido grupo " + Nombre);
            foreach (var d in dispositivos)
            {
                d.Encender();
            }
        }

        public void Apagar()
        {
            Console.WriteLine();
            Console.WriteLine("Apagado grupo " + Nombre);
            foreach (var d in dispositivos)
            {
                d.Apagar();
            }
        }

        public IEnumerable<IDispositivo> ObtenerDispositivosPlano()
        {
            foreach (var d in dispositivos)
            {
                if (d is GrupoDispositivos g)
                {
                    foreach (var sub in g.ObtenerDispositivosPlano())
                        yield return sub;
                }
                else
                {
                    yield return d;
                }
            }
        }
    }
}
