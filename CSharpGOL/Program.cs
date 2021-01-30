using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Diagnostics;
using System.Threading;

namespace CSharpGOL
{
    class Program
    {
        /// <param name="height">Specifies the number of cells vertically as an int</param>
        /// <param name="width">Specifies the number of cells horizontally as an int</param>
        /// <param name="refresh">Specifies the minimum millisecond refresh time between frames as an int</param>
        static void Main(int height = 0, int width = 0, int refresh = 100)
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
            RunSimulation(height, width, refresh);
        }    

        static void RunSimulation(int rowSize, int colSize, int refreshBuffer)
        {
            var simulation = new Simulation(rowSize, colSize);
            var renderer = new Renderer(simulation);           
            
            var stopWatch = new Stopwatch(); 

            renderer.RefreshFrame();

            while (true)
            {
                stopWatch.Start();
                simulation.NextGeneration();
                stopWatch.Stop();

                var bufferTimeDelta = refreshBuffer - (int)stopWatch.ElapsedMilliseconds;
                if (bufferTimeDelta > 0)
                {
                    Thread.Sleep(bufferTimeDelta);
                }

                stopWatch.Reset();

                renderer.RefreshFrame();
            }
        }
    }
}
