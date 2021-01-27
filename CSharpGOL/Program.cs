using System.Diagnostics;
using System.Threading;
using System;

namespace CSharpGOL
{
    class Program
    {
        static void Main(string[] args)
        {   
            int rowSize;
            int colSize;

            Renderer renderer;
            Simulation simulation;

            const int refreshMilliseconds = 100;
            Stopwatch stopWatch = new Stopwatch();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            rowSize = Console.WindowHeight - 3;
            colSize = Console.WindowWidth / 2;

            simulation = new Simulation(rowSize, colSize);
            renderer = new Renderer(simulation);

            renderer.RefreshFrame();  
            
            while (true)
            {
                stopWatch.Start();
                simulation.NextGeneration();
                stopWatch.Stop();
                
                int delta = refreshMilliseconds - (int)stopWatch.ElapsedMilliseconds;
                if (delta > 0)
                {
                    Thread.Sleep(delta);
                }
                stopWatch.Reset();

                renderer.RefreshFrame();
            }
        }
    }
}
