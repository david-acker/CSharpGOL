using CSharpGOL.Core.Config;
using System;
using System.CommandLine;

namespace CSharpGOL.Application.Extensions;

public static class RootCommandExtensions
{
    public static RootCommand AddGridOptions(this RootCommand rootCommand, GridOptions defaultGridOptions)
    {
        int defaultHeight = defaultGridOptions.Height.HasValue
            ? defaultGridOptions.Height.Value
            : Console.WindowHeight - 2;

        rootCommand.Add(
            new Option<int>(
                "--height",
                getDefaultValue: () => defaultHeight,
                description: "The number of cells vertically. Defaults to the console window height."));

        int defaultWidth = defaultGridOptions.Width.HasValue
            ? defaultGridOptions.Width.Value
            : Console.WindowWidth - 1;

        rootCommand.Add(
            new Option<int>(
                "--width",
                getDefaultValue: () => defaultWidth,
                description: "The number of cells horizontally. Defaults to the console window width."));

        return rootCommand;
    }

    public static RootCommand AddRenderingOptions(this RootCommand rootCommand, RenderingOptions defaultRenderingOptions)
    {
        rootCommand.Add(
            new Option<char>(
                "--living-cell",
                getDefaultValue: () => defaultRenderingOptions.LivingCell,
                description: "The display character for a living cell."));

        rootCommand.Add(
            new Option<char>(
               "--dead-cell",
                getDefaultValue: () => defaultRenderingOptions.DeadCell,
                description: "The display character for a dead cell."));

        rootCommand.Add(
            new Option<char>(
                "--header",
                getDefaultValue: () => defaultRenderingOptions.Header,
                description: "The display character for the header."));

        rootCommand.Add(
             new Option<bool>(
                "--show-header",
                getDefaultValue: () => defaultRenderingOptions.ShowHeader,
                description: "Whether to display the current generation header."));

        rootCommand.Add(
            new Option<int>(
                "--look-back-window",
                getDefaultValue: () => defaultRenderingOptions.LookBackWindow,
                description: "The number of previous frames included when checking if the simulation has reached a steady state."));

        return rootCommand;
    }

    public static RootCommand AddSimulationOptions(this RootCommand rootCommand, SimulationOptions defaultSimulationOptions)
    {
        rootCommand.Add(
            new Option<bool>(
                "--loop",
                getDefaultValue: () => defaultSimulationOptions.Loop,
                description: "Whether to restart the simulation from it's initial state, after completion."));

        rootCommand.Add(
            new Option<int>(
                "--density",
                getDefaultValue: () => defaultSimulationOptions.Density,
                description: "The density of living cells when randomly generated. Ranges from 1 (min) to 9 (max)."));

        rootCommand.Add(
            new Option<bool>(
                "--randomize-density",
                getDefaultValue: () => defaultSimulationOptions.RandomizeDensity,
                description: "Whether to use a random density every time the simulation restarts."));

        rootCommand.Add(
            new Option<int>(
                "--refresh-rate",
                getDefaultValue: () => defaultSimulationOptions.RefreshRate,
                description: "The minimum time between each frame, in milliseconds."));

        rootCommand.Add(
            new Option<int?>(
                "--seed",
                getDefaultValue: () => defaultSimulationOptions.Seed,
                description: "An optional integer used to seed the random number generator."));

        return rootCommand;
    }
}
