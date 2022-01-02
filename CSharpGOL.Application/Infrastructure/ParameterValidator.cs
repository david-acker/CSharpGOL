using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSharpGOL.Application.Infrastructure;

public static class ParameterValidator
{
    public static IEnumerable<ValidationResult> Validate(object parameterOptions)
    {
        var context = new ValidationContext(parameterOptions);
        var results = new List<ValidationResult>();

        Validator.TryValidateObject(parameterOptions, context, results, validateAllProperties: true);

        return results;
    }
}
