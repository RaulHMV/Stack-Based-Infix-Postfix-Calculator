using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piletita
{
    class Convertidor
    {
        // Funcion que define la precedencia de cada operador.
        // La potencia (^) tiene mayor precedencia.
        private static int Precedencia(char operador)
        {
            if (operador == '+' || operador == '-') return 1;
            if (operador == '*' || operador == '/') return 2;
            if (operador == '^') return 3;
            return 0;
        }

        // Esta funcion convierte una expresion infija a notacion posfija (RPN)
        // La maquina hace la conversion "por su lado" aqui.
        public static string InfijaAPosfija(string infija)
        {
            Pila operadores = new Pila();
            string posfija = "";

            for (int i = 0; i < infija.Length; i++)
            {
                char c = infija[i];

                if (char.IsDigit(c))
                {
                    // Si es un digito, se agrega directamente a la salida posfija
                    posfija += c;
                }
                else if (c == '(')
                {
                    // Se apila el parentesis abierto
                    operadores.Apilar(c);
                }
                else if (c == ')')
                {
                    // Se desapilan los operadores hasta encontrar el '('
                    while (!operadores.EstaVacia() && operadores.Cima() != '(')
                    {
                        posfija += operadores.Desapilar();
                    }
                    // Se quita el '(' de la pila
                    if (!operadores.EstaVacia()) operadores.Desapilar();
                }
                else
                {
                    // Para otros operadores, se evalua la precedencia
                    // Se maneja la asociatividad derecha para '^'
                    while (!operadores.EstaVacia() &&
                           ((c != '^' && Precedencia(operadores.Cima()) >= Precedencia(c)) ||
                            (c == '^' && Precedencia(operadores.Cima()) > Precedencia(c))))
                    {
                        posfija += operadores.Desapilar();
                    }
                    operadores.Apilar(c);
                }
            }

            // Se desapilan los operadores restantes
            while (!operadores.EstaVacia())
            {
                posfija += operadores.Desapilar();
            }

            return posfija;
        }

        // Esta funcion evalua una expresion en notacion posfija.
        // La maquina realiza la operacion "por su lado" usando una pila de enteros.
        public static int EvaluarPosfija(string posfija)
        {
            Stack<int> pila = new Stack<int>();

            for (int i = 0; i < posfija.Length; i++)
            {
                char c = posfija[i];
                if (char.IsDigit(c))
                {
                    // Convierte el caracter digito a entero y lo empuja en la pila
                    pila.Push(c - '0');
                }
                else
                {
                    // La maquina desapila dos operandos y aplica el operador
                    int b = pila.Pop();
                    int a = pila.Pop();
                    int resultado = 0;

                    if (c == '+') resultado = a + b;
                    else if (c == '-') resultado = a - b;
                    else if (c == '*') resultado = a * b;
                    else if (c == '/') resultado = a / b;
                    else if (c == '^') resultado = (int)Math.Pow(a, b);

                    // Empuja el resultado de vuelta a la pila
                    pila.Push(resultado);
                }
            }

            return pila.Pop();
        }

        // Esta funcion evalua la expresion infija directamente, "por su lado"
        // sin convertirla a posfija. Usa dos pilas: una para operandos y otra para operadores.
        public static int EvaluarInfija(string infija)
        {
            Stack<int> operandos = new Stack<int>();   // Pila para los numeros
            Stack<char> operadores = new Stack<char>();  // Pila para los operadores

            for (int i = 0; i < infija.Length; i++)
            {
                char c = infija[i];
                if (char.IsDigit(c))
                {
                    // La maquina toma el digito y lo guarda en la pila de operandos
                    operandos.Push(c - '0');
                }
                else if (c == '(')
                {
                    // Guarda el parentesis abierto en la pila de operadores
                    operadores.Push(c);
                }
                else if (c == ')')
                {
                    // La maquina procesa hasta encontrar el '('
                    while (operadores.Count > 0 && operadores.Peek() != '(')
                    {
                        AplicarOperacion(operandos, operadores);
                    }
                    operadores.Pop(); // Quita el '('
                }
                else
                {
                    // Mientras haya operadores con mayor o igual precedencia,
                    // la maquina los procesa
                    while (operadores.Count > 0 && Precedencia(operadores.Peek()) >= Precedencia(c))
                    {
                        AplicarOperacion(operandos, operadores);
                    }
                    operadores.Push(c);
                }
            }
            // Procesa los operadores que queden en la pila
            while (operadores.Count > 0)
            {
                AplicarOperacion(operandos, operadores);
            }

            // Devuelve el resultado final de la evaluacion infija
            return operandos.Pop();
        }
        // Funcion auxiliar para aplicar una operacion entre dos operandos.
        // Aqui la maquina toma los dos ultimos numeros y el operador, y realiza la operacion.
        private static void AplicarOperacion(Stack<int> operandos, Stack<char> operadores)
        {
            int b = operandos.Pop();
            int a = operandos.Pop();
            char op = operadores.Pop();

            int resultado = 0;
            if (op == '+') resultado = a + b;
            else if (op == '-') resultado = a - b;
            else if (op == '*') resultado = a * b;
            else if (op == '/') resultado = a / b;
            else if (op == '^') resultado = (int)Math.Pow(a, b);

            operandos.Push(resultado);
        }
    }
}
