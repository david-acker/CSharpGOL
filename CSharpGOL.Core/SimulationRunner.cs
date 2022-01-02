using CSharpGOL.Core.Config;
using CSharpGOL.Core.Interfaces;
using System.Diagnostics;

namespace CSharpGOL.Core;

public interface ISimulationRunner
{
    void Run();
}

public class SimulationRunner : ISimulationRunner
{
    private readonly IConsoleWriterService _consoleWriterService;
    private readonly ISimulation _simulation;
    private readonly ISimulationRenderer _simulationRenderer;
    private readonly SimulationOptions _simulationOptions;
    private readonly RenderingOptions _rendererOptions;
    
    private readonly Stopwatch _stopwatch = new Stopwatch();
    private readonly Queue<string> _frameBuffer;

    public SimulationRunner(
        IConsoleWriterService consoleWriterService,
        ISimulation simulation,
        ISimulationRenderer simulationRenderer,
        SimulationOptions simulationOptions,
        RenderingOptions rendererOptions)
{
        _consoleWriterService = consoleWriterService;
        _simulation = simulation;
        _simulationRenderer = simulationRenderer;

        _simulationOptions = simulationOptions;
        _rendererOptions = rendererOptions;

        _frameBuffer = new Queue<string>(5);
    }

    public void Run()
    {
        _stopwatch.Start();
            
        while (true)
        {
            _simulation.MoveForward();

            string header = _rendererOptions.ShowHeader
                ? _simulationRenderer.RenderHeader()
                : string.Empty;
            string frame = _simulationRenderer.RenderFrame();

            // TODO: Improve how this "frame buffering" mechanism is handled.
            _stopwatch.Stop();
            BufferFrame();
            _consoleWriterService.Write(header, frame);
            _stopwatch.Restart();

            // TODO: Consider encapsulating the frame comparison in the SimulationRenderer.
            // Also, storing the pre-calculated hashcode of the frame in the FrameBuffer
            // rather than the actual frame (string) itself.
            if (_frameBuffer.Contains(frame))
            {
                if (_simulationOptions.Loop)
                {
                    _simulation.Reset();
                }
                else
                {
                    _simulation.Randomize();
                }

                _frameBuffer.Clear();
            }
            else
            {
                // TODO: Use a circular buffer for this.
                _frameBuffer.Enqueue(frame);
                if (_frameBuffer.Count > 5)
                {
                    _frameBuffer.Dequeue();
                }
            }
        }
    }

    private void BufferFrame()
    {
        int frameDelayBuffer = _simulationOptions.RefreshRate - (int)_stopwatch.ElapsedMilliseconds;

        if (frameDelayBuffer > 0)
        {
            Thread.Sleep(frameDelayBuffer);
        }
    }
}
