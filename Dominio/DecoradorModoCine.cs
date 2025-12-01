using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DecoradorModoCine : DispositivoDecorador
    {
        public DecoradorModoCine(IDispositivo dispositivo) : base(dispositivo)
        {
        }

        public override string NombreDecorador => "Modo Cine";

        public override void Encender()
        {
            base.Encender();
            Console.WriteLine("(Activando configuración de Modo Cine en " + Nombre + ": brillo optimizado, sonido envolvente)");
        }

        public override void Apagar()
        {
            Console.WriteLine("(Saliendo de Modo Cine en " + Nombre + ")");
            base.Apagar();
        }
    }
}
