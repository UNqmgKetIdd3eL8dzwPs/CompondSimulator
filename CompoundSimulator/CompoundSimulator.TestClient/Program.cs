using System;
using System.Collections.Generic;
using System.Threading;
using CompoundSimulator.Library;
using CompoundSimulator.Library.Interfaces;
using CompoundSimulator.TestClient.Strategies;

namespace CompoundSimulator.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var market = new Market();

            var strategySettings = new ProvideLiquidityAndStakeSettings
            {
                LiquidityPairAPR = 0.7335,
                TokenStakingAPR = 1.6468,
                LPBonusWithdrawalFee = new Crypto(1.0m, market["CRO"]),
                TokenRestakingFee = new Crypto(1.0m, market["CRO"]),
                TokenStakingFee = new Crypto(1.0m, market["CRO"]),
            };

            var strategyInitialState = new ProvideLiquidityAndStakeState
            {
                LiquidityPair = new Tuple<Crypto, Crypto>(new Crypto(0.0988978m, market["WETH"]),
                    new Crypto(737.622m, market["CRO"])),
                LiquidityPairBonusAccumulated = new Crypto(122107.77m, market["VVS"]),
                StakedTokens = new Crypto(341397.915m, market["VVS"]),
                TotalFeesPaid = new Crypto(0.0m, market["CRO"]),
                StakingBonusAccumulated = new Crypto(26250.89545m, market["VVS"]),
                Wallet = new Crypto(0.0m, market["EUR"]),
            };

            var strategy = new ProvideLiquidityAndRestake(strategySettings, strategyInitialState, market);

            var maxStrategyResult = new Crypto(0.0m, market["EUR"]);
            var lpRestakePeriodForOptimalStrategy = 0;
            var vvsRestakePeriodForOptimalStrategy = 0;

            for (var lpBonusRestaking = 1; lpBonusRestaking < 500; lpBonusRestaking++)
            {
                for (var vvsRestaking = 1; vvsRestaking < 500; vvsRestaking++)
                {

                    strategy.Initialize(strategyInitialState);
                    var scheduleBasedSolution =
                        ProvideLiquidityAndStakeSolution.NewScheduledSolution(lpBonusRestaking, vvsRestaking, 365);
                    var randomSolution = ProvideLiquidityAndStakeSolution.NewRandomSolution(365);
                    var result = strategy.ExecuteForSolution(randomSolution, 365);

                    if (result.ConvertTo(market["EUR"]).Amount > maxStrategyResult.ConvertTo(market["EUR"]).Amount)
                    {
                        maxStrategyResult = result;
                        lpRestakePeriodForOptimalStrategy = lpBonusRestaking;
                        vvsRestakePeriodForOptimalStrategy = vvsRestaking;
                    }
                }
            }

            Console.WriteLine($"Optimal strategy: Collect LP bonus every {lpRestakePeriodForOptimalStrategy} day(s), restake vvs every {vvsRestakePeriodForOptimalStrategy} day(s), this will yield {maxStrategyResult.ConvertTo(market["EUR"])} in 1 year");

        }
    }
}
