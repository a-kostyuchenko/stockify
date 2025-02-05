using Stockify.Common.Domain;

namespace Stockify.Modules.Stocks.Domain.Stockholders;

public sealed class Stockholder : Entity<StockholderId>
{
    private Stockholder()
    {
    }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public RiskProfile RiskProfile { get; private set; }
    
    public static Stockholder Create(StockholderId id, string firstName, string lastName, string email)
    {
        return new Stockholder
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            RiskProfile = RiskProfile.Empty
        };
    }
    
    public void SpecifyRiskProfile(decimal coefficient, string attitudeType) => 
        RiskProfile = RiskProfile.Create(coefficient, attitudeType);
}
