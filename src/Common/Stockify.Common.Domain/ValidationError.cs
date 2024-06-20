namespace Stockify.Common.Domain;

public sealed record ValidationError(Error[] Errors)
    : Error(
        "General.Validation",
        "One or more validation errors occurred",
        ErrorType.Validation);
