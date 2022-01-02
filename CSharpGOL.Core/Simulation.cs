using CSharpGOL.Core.Config;

namespace CSharpGOL.Core
{
    public interface ISimulation
    {
        int Height { get; }
        int Width { get; }
        int Generation { get; }

        void MoveForward();
        void Reset();
        void Randomize();

        bool GetCellState(int rowIndex, int columnIndex);
    }

    public class Simulation : ISimulation
    {
        private readonly IGrid _grid;
        private readonly SimulationOptions _options;

        private readonly Random _random;

        public int Height => _grid.Height;
        public int Width => _grid.Width;

        public int Generation { get; set; } = 0;

        public Simulation(IGrid grid, Random random, SimulationOptions options)
        {
            _grid = grid;
            _random = random;

            _options = options;

            Randomize();
            _grid.SetInitialState();
        }

        public void MoveForward()
        {
            Generation++;
            _grid.MoveForward();
        }

        public void Reset()
        {
            Generation = 0;
            _grid.Reset();
        }

        public void Randomize()
        {
            Generation = 0;

            int density = _options.RandomizeDensity
                ? _random.Next(11)
                : _options.Density;
            
            _grid.Randomize(density);
        }

        public bool GetCellState(int rowIndex, int columnIndex)
        {
            return _grid[rowIndex, columnIndex];
        }
    }
}
