using System.ComponentModel.DataAnnotations;

namespace CSharpGOL.Core.Config;

public class SimulationOptions
{
    public bool Loop { get; set; }

    [Range(1, 9, ErrorMessage = "The density must be between {0} and {1}.")]
    public int Density { get; set; }

    public bool RandomizeDensity { get; set; }

    // TODO: Consider moving to RenderingOptions.

    [Range(0, int.MaxValue, ErrorMessage = "The refresh rate must be between {0} and {1}.")]
    public int RefreshRate { get; set; }

    public int? Seed { get; set; }
}
