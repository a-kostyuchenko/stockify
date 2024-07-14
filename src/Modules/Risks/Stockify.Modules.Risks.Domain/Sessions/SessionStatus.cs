using Stockify.Common.Domain;

namespace Stockify.Modules.Risks.Domain.Sessions;

public sealed class SessionStatus(int id, string name) : Enumeration<SessionStatus>(id, name)
{
    public static readonly SessionStatus Draft = new(1, "draft");
    public static readonly SessionStatus Active = new(2, "active");
    public static readonly SessionStatus Completed = new(3, "completed");
}
