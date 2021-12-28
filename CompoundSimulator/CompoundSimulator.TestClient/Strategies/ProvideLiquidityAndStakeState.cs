using System;
using CompoundSimulator.Library;
using CompoundSimulator.Library.Interfaces;

namespace CompoundSimulator.TestClient.Strategies
{
    public class ProvideLiquidityAndStakeState : IInvestmentStrategyState
    {
        public Tuple<Crypto, Crypto> LiquidityPair { get; set; }
        public Crypto LiquidityPairBonusAccumulated { get; set; }
        public Crypto TotalFeesPaid { get; set; }
        public Crypto Wallet { get; set; }
        public Crypto StakingBonusAccumulated { get; set; }
        public Crypto StakedTokens { get; set; }
    }
}