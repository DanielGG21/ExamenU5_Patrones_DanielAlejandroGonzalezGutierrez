using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DecoradorModoNocturno : DispositivoDecorador
    {
        public DecoradorModoNocturno(IDispositivo dispositivo) : base(dispositivo)
        {
        }

        public override string NombreDecorador => "Modo nocturno";

        public override void Encender()
        {
            base.Encender();
            Console.WriteLine("(Ajustando brillo y volumen para modo nocturno en " + Nombre + ")");
        }

        public override void Apagar()
        {
            Console.WriteLine("(Restaurando configuraciones normales de " + Nombre + ")");
            base.Apagar();
        }
    }
}
