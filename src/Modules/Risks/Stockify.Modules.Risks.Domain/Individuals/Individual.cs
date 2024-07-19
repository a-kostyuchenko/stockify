using Stockify.Common.Domain;
using Stockify.Modules.Risks.Domain.Individuals.Events;
using Stockify.Modules.Risks.Domain.Sessions;

namespace Stockify.Modules.Risks.Domain.Individuals;

public class Individual : Entity<IndividualId>
{
    private Individual()
    {
    }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public RiskAttitude Attitude { get; private set; }
    
    public static Individual Create(IndividualId id, string firstName, string lastName, string email)
    {
        return new Individual
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Attitude = RiskAttitude.Unspecified
        };
    }
    
    public Result EstimateRiskAttitude(Session session)
    {
        if (session.IndividualId != Id)
        {
            return Result.Failure(SessionErrors.IndividualMismatch);
        }
        
        Result<RiskAttitude> result = RiskAttitude.Estimate(session.TotalPoints, session.MaxPoints);

        if (result.IsFailure)
        {
            return result;
        }

        Attitude = result.Value;
        
        Raise(new RiskAttitudeEstimatedDomainEvent(Id));

        return Result.Success();
    }
}
