namespace Core.Dtos.Exceptions.Partner;

public class PartnerPhoneAlreadyRegistered : Exception
{
    public PartnerPhoneAlreadyRegistered()
        : base("") { }
    public PartnerPhoneAlreadyRegistered(string message)
        : base(message) { }
}
