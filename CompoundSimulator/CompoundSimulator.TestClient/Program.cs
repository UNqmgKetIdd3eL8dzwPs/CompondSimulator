using System;
using System.Threading;
using CompoundSimulator.Library;

namespace CompoundSimulator.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var euro = new Token()
            {
                Name = "EUR",
                CurrentPrice = 1,
            };

            var cro = new Token()
            {
                Name = "CRO",
                DecimalPlaces = 3,
                CurrentPrice = 0.5222m,
            };

            Console.WriteLine($"CRO price is {cro.GetPriceIn(euro)} {euro.Name}");
            Console.WriteLine($"EUR price is {euro.GetPriceIn(cro)} {cro.Name}");
            Console.WriteLine($"EUR price is {euro.GetPriceStringIn(cro)} {cro.Name}");
        }
    }
}
