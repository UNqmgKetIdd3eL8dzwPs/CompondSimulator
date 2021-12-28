using System;
using System.Collections.Generic;
using System.Linq;
using CompoundSimulator.Library.Interfaces;

namespace CompoundSimulator.TestClient.Strategies
{
    public class ProvideLiquidityAndStakeSolution : IInvestmentStrategySolution
    {
        public Dictionary<int, bool> CollectLPBonusAndStakeSchedule { get; set; }
        public Dictionary<int, bool> RestakeTokenSchedule { get; set; }

        public ProvideLiquidityAndStakeSolution()
        {
            this.CollectLPBonusAndStakeSchedule = new Dictionary<int, bool>();

            this.RestakeTokenSchedule = new Dictionary<int, bool>();
        }

        public static IInvestmentStrategySolution NewRandomSolution(int numberOfDays)
        {
            var result = new ProvideLiquidityAndStakeSolution();
            var random = new Random();


            for (var i = 0; i < numberOfDays; i++)
            {
                result.CollectLPBonusAndStakeSchedule[i] = (random.Next(2) == 1);
                result.RestakeTokenSchedule[i] = (random.Next(2) == 1);
            }

            return result;
        }

        public static IInvestmentStrategySolution NewScheduledSolution(int collectLPAndStakeInterval, int restakeTokenSchedule, int numberOfDays)
        {
            if (numberOfDays < 1)
            {
                throw new ArgumentException("Can't generate schedule for non-positive period", nameof(numberOfDays));
            }

            if (collectLPAndStakeInterval < 0)
            {
                throw new ArgumentException("Can't generate schedule with negative interval", nameof(collectLPAndStakeInterval));
            }
            
            if (restakeTokenSchedule < 0)
            {
                throw new ArgumentException("Can't generate schedule with negative interval", nameof(restakeTokenSchedule));
            }

            return new ProvideLiquidityAndStakeSolution()
            {
                CollectLPBonusAndStakeSchedule = Enumerable.Range(0, numberOfDays)
                    .ToDictionary(i => i, i => collectLPAndStakeInterval != 0 && (i % collectLPAndStakeInterval == 0)),
                RestakeTokenSchedule = Enumerable.Range(0, numberOfDays)
                    .ToDictionary(i => i, i => restakeTokenSchedule != 0 && (i % restakeTokenSchedule == 0)),
            };
        }
    }
}