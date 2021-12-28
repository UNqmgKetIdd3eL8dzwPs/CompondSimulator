using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CompoundSimulator.Library.Interfaces;

namespace CompoundSimulator.Library
{
    public class GeneticAlgorithmEngine
    {
        public double MutationProbability { get; set; }
        public int GenerationSize { get; set; }
        public IDictionary<int, IEnumerable<IInvestmentStrategySolution>> Generations { get; set; }
        public int CurrentGenerationIndex => Generations.Max(g => g.Key);
        public IEnumerable<IInvestmentStrategySolution> CurrentGeneration => Generations[CurrentGenerationIndex];
        public IInvestmentStrategy Strategy { get; set; }
        public IInvestmentStrategyState StrategyInitialState { get; set; }
        public int StrategyLength { get; set; }

        public IEnumerable<IInvestmentStrategySolution> GenerateRandomGeneration()
        {
            return Enumerable.Range(0, GenerationSize)
                .Select(i => Strategy.GenerateRandomSolution(StrategyLength)).ToList();
        }
    }
}