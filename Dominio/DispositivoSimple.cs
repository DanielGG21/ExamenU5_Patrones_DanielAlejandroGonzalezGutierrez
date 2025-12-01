using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DispositivoSimple : IDispositivo
    {
        public string Nombre { get; }

        private bool encendido;

        public bool EstaEncendido => encendido;

        public DispositivoSimple(string nombre)
        {
            Nombre = nombre;
            encendido = false;
        }

        public virtual void Encender()
        {
            if (!encendido)
            {
                encendido = true;
                Console.WriteLine("--- " + Nombre + " Encendido");
            }
        }

        public virtual void Apagar()
        {
            if (encendido)
            {
                encendido = false;
                Console.WriteLine("--- " + Nombre + " Apagado");
            }
        }
    }
}
