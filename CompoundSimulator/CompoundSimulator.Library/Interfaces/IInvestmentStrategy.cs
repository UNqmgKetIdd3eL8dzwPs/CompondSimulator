using System.Resources;

namespace CompoundSimulator.Library.Interfaces
{
    public interface IInvestmentStrategy
    {
        public void Initialize(IInvestmentStrategyState state);
        public Crypto Total { get; }
        public Crypto ExecuteForSolution(IInvestmentStrategySolution solution, int numberOfDays);
    }
}