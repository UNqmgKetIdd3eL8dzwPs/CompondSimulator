using System;
using System.Collections.Generic;
using System.Threading;
using CompoundSimulator.Library;

namespace CompoundSimulator.TestClient
{
    class Program
    {
        public static Dictionary<string, Token> Market = new Dictionary<string, Token>()
        {
            {"EUR", new Token { Symbol = "EUR", CurrentPrice = 1,}},
            {"CRO", new Token { Symbol = "CRO", CurrentPrice = 0.5222m, DecimalPlaces = 3}}, 
        };

        static void Main(string[] args)
        {
            var croDeposit = new Crypto(4000, Market["CRO"]);
            var euroDeposit = new Crypto(1234, Market["EUR"]);

            Console.WriteLine($"{croDeposit} + {euroDeposit} == {(euroDeposit + croDeposit).ConvertTo(Market["CRO"])}");
        }
    }
}
