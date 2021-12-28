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
        public IDictionary<int, IList<IInvestmentStrategySolution>> Generations { get; set; }
        public int CurrentGenerationIndex => Generations.Max(g => g.Key);
        public IList<IInvestmentStrategySolution> CurrentGeneration => Generations[CurrentGenerationIndex];
        public IInvestmentStrategy StrategyToVerify { get; set; }
        public IInvestmentStrategyState StrategyInitialState { get; set; }
    }
}