using System.ComponentModel.DataAnnotations;

namespace CSharpGOL.Core.Config;

public class GridOptions
{
    [Range(1, int.MaxValue, ErrorMessage = "The grid height must be between {0} and {1}.")]
    public int? Height { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "The grid height must be between {0} and {1}.")]
    public int? Width { get; set; }
}
