using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Individuals;

public class Individual : Entity<IndividualId>
{
    private Individual()
    {
    }
    
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    
    public static Individual Create(IndividualId id, string firstName, string lastName, string email)
    {
        return new Individual
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
    }
}
