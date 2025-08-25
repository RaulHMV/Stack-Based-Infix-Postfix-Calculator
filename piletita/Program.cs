using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piletita
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("\nMenusito :3");
                Console.WriteLine("1. Convertir infija a posfija y evaluar");
                Console.WriteLine("2. Salir");
                Console.Write("Elige tu opción: ");
                string opcion = Console.ReadLine();

                if (opcion == "1")
                {
                    Console.Write("Ingresa una expresión infija: ");
                    string infija = Console.ReadLine();

                    // La máquina convierte la infija a posfija "por su lado"
                    string posfija = Convertidor.InfijaAPosfija(infija);

                    // La máquina evalúa la infija directamente, sin convertirla
                    int resultadoInfija = Convertidor.EvaluarInfija(infija);

                    // La máquina evalúa la posfija (resultado de la conversión)
                    int resultadoPosfija = Convertidor.EvaluarPosfija(posfija);

                    Console.WriteLine($"Expresión Infija: {infija}");
                    Console.WriteLine($"Expresión Posfija: {posfija}");
                    Console.WriteLine($"Resultado (Evaluación directa de Infija): {resultadoInfija}  // La máquina resuelve la infija por su lado");
                    Console.WriteLine($"Resultado (Evaluación de Posfija): {resultadoPosfija}  // La máquina resuelve la posfija");

                    // Compara ambos resultados para confirmar que coinciden
                    if (resultadoInfija == resultadoPosfija)
                        Console.WriteLine("Los resultados coinciden.");
                    else
                        Console.WriteLine("Error: Los resultados no coinciden, revisa la operación.");
                }
                else if (opcion == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opción no válida, intenta de nuevo.");
                }
            }
        }
    }
}
