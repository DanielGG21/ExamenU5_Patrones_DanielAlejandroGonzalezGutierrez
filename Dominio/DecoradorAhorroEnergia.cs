using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DecoradorAhorroEnergia : DispositivoDecorador
    {
        public DecoradorAhorroEnergia(IDispositivo dispositivo) : base(dispositivo)
        {
        }

        public override string NombreDecorador => "Ahorro de energía";

        public override void Encender()
        {
            base.Encender();
            Console.WriteLine("(Activando modo ahorro de energía en " + Nombre + ")");
        }

        public override void Apagar()
        {
            Console.WriteLine("(Guardando consumo de energía de " + Nombre + ")");
            base.Apagar();
        }
    }
}
