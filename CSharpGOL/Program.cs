using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.Threading;

namespace CSharpGOL
{
    class Program
    {
        /// <param name="height">Specifies the number of cells vertically as an int.</param>
        /// <param name="width">Specifies the number of cells horizontally as an int.</param>
        /// <param name="refresh">Specifies the minimum millisecond refresh time between frames as an int.</param>
        /// <param name="auto">Specifies if the simulation will automatically progress forward.</param>
        static void Main(int height = 0, int width = 0, int refresh = 100, bool auto = true)
        {   
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            if (height <= 0)
                height = Console.WindowHeight - 3;
            
            if (width <= 0)
                width = Console.WindowWidth / 2;

            Console.CancelKeyPress += (sender, EventArgs) =>
            {
                Console.CursorVisible = true;
                Environment.Exit(0);
            };

            Console.CursorVisible = false;

            var simulation = new Simulation(height, width);
            var renderer = new Renderer(simulation);

            RunSimulation(simulation, renderer, refresh, auto);
        }    

        static void RunSimulation(Simulation simulation, Renderer renderer, int refreshBuffer, bool isAuto)
        {
            var stopWatch = new Stopwatch();

            // Display the initial Grid state.
            renderer.RefreshFrame();

            while (true)
            {
                if (!isAuto)
                    Console.ReadLine();

                stopWatch.Start();
                simulation.NextGeneration();
                stopWatch.Stop();

                var timeDeltaBuffer = refreshBuffer - (int)stopWatch.ElapsedMilliseconds;
                if (timeDeltaBuffer > 0)
                    Thread.Sleep(timeDeltaBuffer);
                
                stopWatch.Reset();

                renderer.RefreshFrame();
            }
        }
    }
}
