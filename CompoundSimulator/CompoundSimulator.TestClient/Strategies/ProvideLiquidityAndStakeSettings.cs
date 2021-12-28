using CompoundSimulator.Library;
using CompoundSimulator.Library.Interfaces;

namespace CompoundSimulator.TestClient.Strategies
{
    public class ProvideLiquidityAndStakeSettings : IInvestmentStrategySettings
    {
        public double LiquidityPairAPR { get; set; }
        public Crypto LPBonusWithdrawalFee { get; set; }
        public Crypto TokenStakingFee { get; set; }
        public double TokenStakingAPR { get; set; }
        public Crypto TokenRestakingFee { get; set; }
    }
}