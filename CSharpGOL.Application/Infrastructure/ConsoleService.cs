using CSharpGOL.Core.Interfaces;
using System;
using System.Text;

namespace CSharpGOL.Application.Infrastructure;

// Is this really necessary?
public interface IConsoleEnvironmentService
{
    void Initialize();
    void Revert();
}

// TODO: Figure out a better way to handle this, since Console and Environment
// aren't completely encapsulated in this in service anyway.
public class ConsoleService : IConsoleEnvironmentService, IConsoleWriterService
{
    private Encoding _originalEncoding { get; set; }

    public void Initialize()
    {
        _originalEncoding = Console.OutputEncoding;

        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;

        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            Revert();
            Environment.Exit(0);
        };

        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Clear();
    }

    public void Revert()
    {
        Console.OutputEncoding = _originalEncoding;
        Console.CursorVisible = true;
    }

    public void Write(params string[] values)
    {
        Console.SetCursorPosition(0, 0);

        foreach (string value in values)
        {
            Console.Write(value);
        }
    }
}

