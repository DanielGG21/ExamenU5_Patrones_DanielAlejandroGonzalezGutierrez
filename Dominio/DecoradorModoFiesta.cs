using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DecoradorModoFiesta : DispositivoDecorador
    {
        public DecoradorModoFiesta(IDispositivo dispositivo) : base(dispositivo)
        {
        }

        public override string NombreDecorador => "Modo Fiesta";

        public override void Encender()
        {
            base.Encender();
            Console.WriteLine("(Activando Modo Fiesta en " + Nombre + ": luces dinámicas, música intensa)");
        }

        public override void Apagar()
        {
            Console.WriteLine("(Desactivando Modo Fiesta en " + Nombre + ")");
            base.Apagar();
        }
    }
}
