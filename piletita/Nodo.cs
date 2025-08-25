using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piletita
{
    class Nodo
    {
        public char dato;
        public Nodo siguiente;
        public Nodo(char dato)
        {
            this.dato = dato;
            this.siguiente = null;
        }
    }

}
