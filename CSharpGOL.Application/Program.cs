using CSharpGOL.Application.Extensions;
using CSharpGOL.Application.Infrastructure;
using CSharpGOL.Core;
using CSharpGOL.Core.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CSharpGOL.Application;

public class Program
{
    public static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        // TODO: Separately validate all default options prior to setting up to root command.
        GridOptions defaultGridOptions = config.GetSection("Grid").Get<GridOptions>();
        RenderingOptions defaultRenderingOptions = config.GetSection("Rendering").Get<RenderingOptions>();
        SimulationOptions defaultSimulationOptions = config.GetSection("Simulation").Get<SimulationOptions>();

        RootCommand root = new RootCommand()
            .AddGridOptions(defaultGridOptions)
            .AddRenderingOptions(defaultRenderingOptions)
            .AddSimulationOptions(defaultSimulationOptions);

        root.Handler = CommandHandler.Create<GridOptions, SimulationOptions, RenderingOptions>(Run);

        root.Invoke(args);
    }

    private static void Run(
        GridOptions gridOptions,
        SimulationOptions simulationOptions, 
        RenderingOptions renderingOptions)
    {
        // TODO: Clean up (user provided) parameter validation.
        IEnumerable<ValidationResult> validationResults = ValidateParameters(
            gridOptions,
            simulationOptions,
            renderingOptions);

        if (validationResults.Any())
        {
            Console.WriteLine("The provided parameters options container the following errors:");
            foreach (var error in validationResults)
            {
                Console.WriteLine(error);
            }
            Environment.Exit(-1);
        }

        // TODO: Clean up service instantiation.
        var consoleService = new ConsoleService();

        // TODO: Improve how options are post-configuring and
        // the handling of options that dependent on other option values.
        if (!renderingOptions.ShowHeader)
        {
            gridOptions.Height += 1;
        }

        var random = simulationOptions.Seed.HasValue
            ? new Random(simulationOptions.Seed.Value)
            : new Random();

        var simulation = new Simulation(new Grid(random, gridOptions), random, simulationOptions);
        var simulationRenderer = new SimulationRenderer(simulation, renderingOptions);

        var simulationRunner = new SimulationRunner(
            consoleService,
            simulation,
            simulationRenderer,
            simulationOptions,
            renderingOptions);

        // TODO: Add basic exception handling.
        try
        {
            consoleService.Initialize();

            simulationRunner.Run();
        }
        finally
        {
            consoleService.Revert();
        }
    }

    // TODO: Move to ParameterValidator.
    private static IEnumerable<ValidationResult> ValidateParameters(params object[] parameterOptions)
    {
        return parameterOptions.SelectMany(p => ParameterValidator.Validate(p));
    }
}
