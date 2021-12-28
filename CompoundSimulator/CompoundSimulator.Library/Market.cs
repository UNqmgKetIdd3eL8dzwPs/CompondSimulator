using System.Collections.Generic;

namespace CompoundSimulator.Library
{
    public class Market : Dictionary<string, Token>
    {
        public Market()
        {
            // todo, remove in the future, implement initialization from internet
            this.Add("EUR", new Token { Symbol = "EUR", CurrentPrice = 1 });
            this.Add("CRO", new Token { Symbol = "CRO", CurrentPrice = 0.56407380m, DecimalPlaces = 6 });
            this.Add("WETH", new Token { Symbol = "WETH", CurrentPrice = 3617.4m, DecimalPlaces = 2 });
            this.Add("VVS", new Token { Symbol = "VVS", CurrentPrice = 0.00005961m, DecimalPlaces = 8 });
        }
    }
}