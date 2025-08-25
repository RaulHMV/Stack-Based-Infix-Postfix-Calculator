using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piletita
{
    class Pila
    {
        private Nodo cima;

        public Pila()
        {
            cima = null;
        }

        public bool EstaVacia()
        {
            return cima == null;
        }

        public void Apilar(char dato)
        {
            Nodo nuevo = new Nodo(dato);
            nuevo.siguiente = cima;
            cima = nuevo;
        }

        public char Desapilar()
        {
            if (EstaVacia())
                throw new InvalidOperationException("La pila está vacía");
            char dato = cima.dato;
            cima = cima.siguiente;
            return dato;
        }

        public char Cima()
        {
            if (EstaVacia())
                throw new InvalidOperationException("La pila está vacía");
            return cima.dato;
        }
    }

}
