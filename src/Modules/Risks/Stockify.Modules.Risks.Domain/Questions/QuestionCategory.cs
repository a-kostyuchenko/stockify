using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Questions;

public sealed class QuestionCategory : Enumeration<QuestionCategory>
{
    public static readonly QuestionCategory RiskTolerance = new(1, "risk_tolerance");
    public static readonly QuestionCategory LossTolerance = new(2, "loss_tolerance");
    public static readonly QuestionCategory InvestmentHorizon = new(3, "investment_horizon");
    public static readonly QuestionCategory IncomeStability = new(4, "income_stability");
    public static readonly QuestionCategory FinancialKnowledge = new(5, "financial_knowledge");
    public static readonly QuestionCategory InvestmentExperience = new(6, "investment_experience");
    
    private QuestionCategory(int id, string name) : base(id, name)
    {
    }
}
