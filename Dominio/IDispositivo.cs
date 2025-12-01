using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public interface IDispositivo
    {
        string Nombre { get; }
        bool EstaEncendido { get; }
        void Encender();
        void Apagar();
    }
}
