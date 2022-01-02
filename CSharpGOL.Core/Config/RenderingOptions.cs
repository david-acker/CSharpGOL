using System.ComponentModel.DataAnnotations;

namespace CSharpGOL.Core.Config;

public class RenderingOptions
{
    public char LivingCell { get; set; }

    public char DeadCell { get; set; }

    public char Header { get; set; }

    public bool ShowHeader { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "The look back window must be between {0} and {1}.")]
    public int LookBackWindow { get; set; }
}

