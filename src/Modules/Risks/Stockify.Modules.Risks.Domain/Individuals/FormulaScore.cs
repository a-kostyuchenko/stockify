namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed record FormulaScore
{
    private FormulaScore(decimal value, bool isValid)
    {
        Value = value;
        IsValid = isValid;
    }

    public FormulaScore(decimal value) : this(value, true)
    {
    }
    
    public decimal Value { get; }
    public bool IsValid { get; }
    
    public static readonly FormulaScore Invalid = new(0, false);
}