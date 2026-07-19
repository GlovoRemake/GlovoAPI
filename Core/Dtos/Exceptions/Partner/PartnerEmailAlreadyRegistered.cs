namespace Core.Dtos.Exceptions.Partner;

public class PartnerEmailAlreadyRegistered : Exception
{
    public PartnerEmailAlreadyRegistered()
        : base("") { }
    public PartnerEmailAlreadyRegistered(string message)
        : base(message) { }
}
