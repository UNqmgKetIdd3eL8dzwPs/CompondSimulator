namespace CompoundSimulator.Library
{
    public class Token
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public decimal CurrentPrice { get; set; }
        public int DecimalPlaces { get; set; } = 5;
        public decimal GetPriceIn(Token token)
        {
            return this.CurrentPrice / token.CurrentPrice;
        }

        public string GetPriceStringIn(Token token)
        {
            return string.Format("{0:F" + token.DecimalPlaces + "}", this.CurrentPrice / token.CurrentPrice);
        }
    }
}