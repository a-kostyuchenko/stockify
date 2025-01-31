using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public sealed class FormulaType : Enumeration<FormulaType>
{
    public static readonly FormulaType Basic = new(1, "Basic", "B");
    public static readonly FormulaType VaR = new(2, "Value at Risk", "VaR");
    public static readonly FormulaType Camp = new(3, "Capital Asset Pricing Model", "CAMP");
    public static readonly FormulaType PrattArrow = new(4, "Pratt-Arrow Relative Risk Aversion", "Pratt-Arrow RRA");
    public static readonly FormulaType BlackScholes = new(5, "Black Scholes", "BS");
    
    private FormulaType(int id, string name, string abbreviation) : base(id, name)
    {
        Abbreviation = abbreviation;
    }

    public string Abbreviation { get; private set; }
}
