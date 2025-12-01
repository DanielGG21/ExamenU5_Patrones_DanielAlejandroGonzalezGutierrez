using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class DispositivoDecorador : IDispositivo
    {
        protected IDispositivo dispositivo;

        protected DispositivoDecorador(IDispositivo dispositivo)
        {
            this.dispositivo = dispositivo;
        }

        public IDispositivo Inner => dispositivo;

        public virtual string Nombre => dispositivo.Nombre;

        public virtual bool EstaEncendido => dispositivo.EstaEncendido;

        public virtual string NombreDecorador => "Decorador";

        public virtual void Encender()
        {
            dispositivo.Encender();
        }

        public virtual void Apagar()
        {
            dispositivo.Apagar();
        }
    }
}
