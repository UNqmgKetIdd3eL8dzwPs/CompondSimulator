using System;
using System.Collections;
using CompoundSimulator.Library;
using CompoundSimulator.Library.Interfaces;

namespace CompoundSimulator.TestClient.Strategies
{
    public class ProvideLiquidityAndRestake : IInvestmentStrategy
    {
        public void Initialize(IInvestmentStrategyState state)
        {
            var st = (ProvideLiquidityAndStakeState) state;
            LiquidityPair = st.LiquidityPair;
            LiquidityPairBonusAccumulated = st.LiquidityPairBonusAccumulated;
            Wallet = st.Wallet;
            StakedTokens = st.StakedTokens;
            StakingBonusAccumulated = st.StakingBonusAccumulated;
            TotalFeesPaid = st.TotalFeesPaid;
        }

        public Crypto Total => LiquidityPair.Item1
                               + LiquidityPair.Item2
                               + LiquidityPairBonusAccumulated
                               + Wallet
                               + StakedTokens
                               + StakingBonusAccumulated
                               - TotalFeesPaid;

        #region State

        public Tuple<Crypto, Crypto> LiquidityPair { get; set; }
        public Crypto LiquidityPairBonusAccumulated { get; set; }
        public Crypto TotalFeesPaid { get; set; }
        public Crypto Wallet { get; set; }
        public Crypto StakingBonusAccumulated { get; set; }
        public Crypto StakedTokens { get; set; }

        #endregion

        public ProvideLiquidityAndStakeSettings Settings { get; set; }

        public Market Market { get; set; }

        public ProvideLiquidityAndRestake(IInvestmentStrategySettings settings, IInvestmentStrategyState state, Market market)
        {
            Settings = (ProvideLiquidityAndStakeSettings)settings;
            Initialize(state);
        }
        public Crypto ExecuteForSolution(IInvestmentStrategySolution solution, int numberOfDays)
        {
            var strategySolution = (ProvideLiquidityAndStakeSolution)solution;

            for (var i = 0; i < numberOfDays; i++) 
            {
                //1. Generate liquidity pool bonus
                LiquidityPairBonusAccumulated += ((LiquidityPair.Item1 + LiquidityPair.Item2) * Settings.LiquidityPairAPR) / 365;

                //2. Generate staking bonus
                StakingBonusAccumulated += StakedTokens * Settings.TokenStakingAPR / 365;

                //3. Collect LP bonuses and stake
                if (strategySolution.CollectLPBonusAndStakeSchedule[i])
                {
                    StakedTokens += LiquidityPairBonusAccumulated;
                    LiquidityPairBonusAccumulated -= LiquidityPairBonusAccumulated;

                    TotalFeesPaid += Settings.LPBonusWithdrawalFee;
                    TotalFeesPaid += Settings.TokenStakingFee;
                }

                //4. Restake tokens
                if (strategySolution.RestakeTokenSchedule[i])
                {
                    StakedTokens += StakingBonusAccumulated;
                    StakingBonusAccumulated -= StakingBonusAccumulated;

                    TotalFeesPaid += Settings.TokenRestakingFee;
                }
            }

            return Total;
        }
    }
}