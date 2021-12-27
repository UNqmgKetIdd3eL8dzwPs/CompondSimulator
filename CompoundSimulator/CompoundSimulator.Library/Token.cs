namespace CompoundSimulator.Library
{
    public class Token
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public int DecimalPlaces { get; set; } = 2;

        public string GetStringFormat()
        {
            return "{0:F" + this.DecimalPlaces + "}";
        }

        public decimal GetPriceIn(Token token)
        {
            return this.CurrentPrice / token.CurrentPrice;
        }

        public string GetPriceStringIn(Token token)
        {
            return string.Format(token.GetStringFormat(), this.CurrentPrice / token.CurrentPrice);
        }
    }
}