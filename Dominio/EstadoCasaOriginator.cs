using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EstadoCasaOriginator
    {
        private readonly GrupoDispositivos casa;

        public EstadoCasaOriginator(GrupoDispositivos casa)
        {
            this.casa = casa ?? throw new ArgumentNullException(nameof(casa));
        }


        public EscenaCasaMemento CrearMemento(string nombreEscena)
        {
            var estados = new Dictionary<string, bool>();

            foreach (var dispositivo in casa.ObtenerDispositivosPlano())
            {
                if (!estados.ContainsKey(dispositivo.Nombre))
                {
                    estados.Add(dispositivo.Nombre, dispositivo.EstaEncendido);
                }
            }

            return new EscenaCasaMemento(nombreEscena, DateTime.Now, estados);
        }


        public void RestaurarDesdeMemento(EscenaCasaMemento memento)
        {
            if (memento == null) return;

            foreach (var dispositivo in casa.ObtenerDispositivosPlano())
            {
                if (memento.EstadosDispositivos.TryGetValue(dispositivo.Nombre, out bool encender))
                {
                    if (encender && !dispositivo.EstaEncendido)
                    {
                        dispositivo.Encender();
                    }
                    else if (!encender && dispositivo.EstaEncendido)
                    {
                        dispositivo.Apagar();
                    }
                }
            }
        }
    }
}
