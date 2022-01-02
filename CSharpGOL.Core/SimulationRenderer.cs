using CSharpGOL.Core.Config;
using CSharpGOL.Core.Extensions;
using System.Text;

namespace CSharpGOL.Core
{
    public interface ISimulationRenderer
    {
        string RenderHeader();
        string RenderFrame();
    }

    public class SimulationRenderer : ISimulationRenderer
    {
        private readonly ISimulation _simulation;
        private readonly RenderingOptions _options;

        public SimulationRenderer(ISimulation simulation, RenderingOptions options)
        {
            _simulation = simulation;
            _options = options;
        }

        public string RenderHeader()
        {
            return _simulation.Generation.ToString()
                .PadCenter(_simulation.Width, _options.Header) + Environment.NewLine;
        }

        // TODO: Work on reducing the number of times the cells need to be iterated over.
        public string RenderFrame()
        {
            var sb = new StringBuilder(_simulation.Height * (_simulation.Width + 1));

            for (int x = 0; x < _simulation.Height; x++)
            {
                for (int y = 0; y < _simulation.Width; y++)
                {
                    sb.Append(_simulation.GetCellState(x, y) 
                        ? _options.LivingCell 
                        : _options.DeadCell);
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
