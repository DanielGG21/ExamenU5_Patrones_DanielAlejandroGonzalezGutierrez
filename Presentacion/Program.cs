using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion;
using Dominio;



namespace Presentacion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CentroDeControlCasa.Instancia.Iniciar();

        }
    }
}
