using System.Data;

namespace delegateventstestskolenoget
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<int, int, int> beregn = Plus;        // metoden gemmes i variablen (uden parenteser)
            Console.WriteLine(beregn(5, 3));  // 8

            beregn = Gange;                 // nu peger variablen på en anden metode
            Console.WriteLine(beregn(5, 3));  // 15

            beregn = Minus;
            Console.WriteLine(beregn(5, 3)); // 2

            Console.WriteLine(Udregn(5, 3, Plus));
            Console.WriteLine(Udregn(5, 3, Gange));
            Console.WriteLine(Udregn(5, 3, Minus));

            Action<string> besked = SkrivNormal;
            besked("\nBesked med én subscriber"); // Besked kommer én gang grundet 1 sub

            besked += SkrivStort; // Besked kommer to gange grundet 2 subs
            besked("Besked med to subscribers");

            besked -= SkrivStort;
            besked("Farvel mutter"); // Besked kommer kun én gang igen

            Kaffemaskine maskine = new();

            maskine.KaffeKlar += Barista;
            maskine.KaffeKlar += Kunde;
            maskine.Brygg();

            Kaffemaskine lorteSirupsKaffeMaskine = new();
            lorteSirupsKaffeMaskine.Brygg();
        }

        static int Plus(int a, int b) { return a + b; }
        static int Gange(int a, int b) { return a * b; }
        static int Minus(int a, int b) { return a - b; }
        static int Udregn(int a, int b, Func<int, int, int> beregn) { return beregn(a, b); }

        static void SkrivNormal(string besked) { Console.WriteLine(besked); }
        static void SkrivStort(string besked) { Console.WriteLine(besked.ToUpper()); }

        public delegate void Notify();

        public class Kaffemaskine
        {
            public event Notify? KaffeKlar;

            public void Brygg()
            {
                Console.WriteLine("Brygger kaffe...");
                KaffeKlar?.Invoke();
            }
        }

        public static void Barista()
        {
            Console.WriteLine("Barista: Serverer kaffen");
        }

        public static void Kunde()
        {
            Console.WriteLine("Kunde: Tak for kaffen!");
        }
    }
}
