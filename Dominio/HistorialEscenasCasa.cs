using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class HistorialEscenasCasa
    {
        private readonly List<EscenaCasaMemento> escenas = new List<EscenaCasaMemento>();

        public IReadOnlyList<EscenaCasaMemento> Escenas => escenas.AsReadOnly();

        public bool TieneEscenas => escenas.Count > 0;

        public void AgregarEscena(EscenaCasaMemento escena)
        {
            if (escena == null) throw new ArgumentNullException(nameof(escena));
            escenas.Add(escena);
        }

        public EscenaCasaMemento ObtenerEscena(int indice)
        {
            if (indice < 0 || indice >= escenas.Count) return null;
            return escenas[indice];
        }

        public void EliminarEscena(int indice)
        {
            if (indice < 0 || indice >= escenas.Count) return;
            escenas.RemoveAt(indice);
        }

        public void Limpiar()
        {
            escenas.Clear();
        }
    }
}
