using System;
using System.Runtime.CompilerServices;

namespace CompoundSimulator.Library
{
    public class Crypto
    {
        public decimal Amount { get; set; }

        public Token Token { get; set; }

        public Crypto(decimal amount, Token token)
        {
            Amount = amount;
            Token = token;
        }

        public Crypto ConvertTo(Token token)
        {
            return new Crypto(this.Amount * this.Token.GetPriceIn(token), token);
        }

        public override string ToString()
        {
            return $"{string.Format(Token.GetStringFormat(), Amount)} {Token.Symbol}";
        }

        #region Operation oveloads

        public static Crypto operator +(Crypto leftSide, Crypto rightSide)
        {
            var convertedRightSide = rightSide.ConvertTo(leftSide.Token);
            return new Crypto(leftSide.Amount + convertedRightSide.Amount, leftSide.Token);
        }
        
        public static Crypto operator -(Crypto leftSide, Crypto rightSide)
        {
            var convertedRightSide = rightSide.ConvertTo(leftSide.Token);
            return new Crypto(leftSide.Amount - convertedRightSide.Amount, leftSide.Token);
        }

        #region Multiplication

        public static Crypto operator *(Crypto crypto, int multiplier)
        {
            return new Crypto(crypto.Amount * multiplier, crypto.Token);
        }
        
        public static Crypto operator *(int multiplier, Crypto crypto)
        {
            return crypto * multiplier;
        }
        
        public static Crypto operator *(Crypto crypto, decimal multiplier)
        {
            return new Crypto(crypto.Amount * multiplier, crypto.Token);
        }
        
        public static Crypto operator *(decimal multiplier, Crypto crypto)
        {
            return crypto * multiplier;
        }
        
        public static Crypto operator *(Crypto crypto, double multiplier)
        {
            return new Crypto(crypto.Amount * Convert.ToDecimal(multiplier), crypto.Token);
        }
        
        public static Crypto operator *(double multiplier, Crypto crypto)
        {
            return crypto * multiplier;
        }

        #endregion
        
        #region Division

        public static Crypto operator /(Crypto crypto, int divider)
        {
            return new Crypto(crypto.Amount / divider, crypto.Token);
        }

        public static Crypto operator /(Crypto crypto, decimal divider)
        {
            return new Crypto(crypto.Amount / divider, crypto.Token);
        }

        public static Crypto operator /(Crypto crypto, double divider)
        {
            return new Crypto(crypto.Amount / Convert.ToDecimal(divider), crypto.Token);
        }

        #endregion

        #endregion
    }
}