namespace Core.Dtos.Account;

public class VerificationData
{
    public string CodeHash { get; set; } = null!;
    public int AttemptsLeft { get; set; }
}