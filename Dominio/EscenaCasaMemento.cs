using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EscenaCasaMemento
    {
        public string Nombre { get; }
        public DateTime FechaCreacion { get; }
        public Dictionary<string, bool> EstadosDispositivos { get; }

        public EscenaCasaMemento(string nombre, DateTime fechaCreacion, Dictionary<string, bool> estadosDispositivos)
        {
            Nombre = nombre;
            FechaCreacion = fechaCreacion;
            EstadosDispositivos = estadosDispositivos ?? new Dictionary<string, bool>();
        }
    }
}
