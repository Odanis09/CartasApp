using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartasApp
{

    public class Carta
    {
        public string Simbolo { get; }
        public string Valor { get; }

        public Carta(string simbolo, string valor)
        {
            Simbolo = simbolo;
            Valor = valor;
        }

        public override string ToString()
        {
            return Valor + " de " + Simbolo;
        }
    }



    public class Baraja
    {
        private List<Carta> cartas;
        private static readonly string[] Simbolos = { "Corazones", "Diamantes", "Tréboles", "Picas" };
        private static readonly string[] Valores =
        {
            "As", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"
        };

        public Baraja()
        {
            cartas = new List<Carta>();
            CrearBaraja();
        }

        private void CrearBaraja()
        {
            cartas.Clear();
            foreach (string simbolo in Simbolos)
            {
                foreach (string valor in Valores)
                {
                    cartas.Add(new Carta(simbolo, valor));
                }
            }
        }

        public void Barajar()
        {
            Random gnr = new Random();
            int n = cartas.Count;
            while (n > 1)
            {
                n--;
                int k = gnr.Next(n + 1);
                Carta temp = cartas[k];
                cartas[k] = cartas[n];
                cartas[n] = temp;
            }
        }

        public List<Carta> Repartir (int cantidad)
        {
            if (cantidad > cartas.Count)
                throw new InvalidOperationException("No hay suficientes cartas para repartir");

            List<Carta> mano = cartas.GetRange(0, cantidad);
            cartas.RemoveRange(0, cantidad);
            return mano;
        }

        public int CartasRestantes()
        {
            return cartas.Count;
        }
    }
        internal class Program
    {
        static void Main(string[] args)
        {
            Baraja baraja = new Baraja();
            baraja.Barajar();

            Console.Write("¿Cuántos jugadores participarán? ");
            int jugadoresTotales = int.Parse(Console.ReadLine());
            int ronda = 1;

            while (baraja.CartasRestantes() > 0 && jugadoresTotales > 0)
            {
                Console.WriteLine("\n--- Ronda " + ronda + " ---");

                int cantidadCartasPorJugador = 5;
                int jugadoresEstaRonda = jugadoresTotales;

                
                if (jugadoresEstaRonda * cantidadCartasPorJugador > baraja.CartasRestantes())
                {
                    jugadoresEstaRonda = baraja.CartasRestantes() / cantidadCartasPorJugador;
                    if (jugadoresEstaRonda == 0)
                    {
                        Console.WriteLine("No hay suficientes cartas para repartir otra ronda completa.");
                        break;
                    }
                    Console.WriteLine("Solo hay suficientes cartas para " + jugadoresEstaRonda + " jugadores.");
                }

                for (int i = 1; i <= jugadoresEstaRonda; i++)
                {
                    Console.WriteLine("\nCartas para el jugador " + i + ":");
                    var mano = baraja.Repartir(cantidadCartasPorJugador);
                    foreach (var carta in mano)
                    {
                        Console.WriteLine(carta);
                    }
                }

                Console.WriteLine("\nCartas restantes en la baraja: " + baraja.CartasRestantes());

                if (baraja.CartasRestantes() == 0)
                {
                    Console.WriteLine("No quedan más cartas en la baraja.");
                    break;
                }

                Console.Write("\n¿Deseas repartir otra ronda? (si/no): ");
                string respuesta = Console.ReadLine().Trim().ToLower();
                if (respuesta != "si")
                {
                    Console.WriteLine("Reparto finalizado por el usuario.");
                    break;
                }

                Console.Write("¿Cuántos jugadores recibirán cartas en la próxima ronda? ");
                jugadoresTotales = int.Parse(Console.ReadLine());
                ronda++;
            }

            Console.WriteLine("\nFin del programa.");
        }
    }
}