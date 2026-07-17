namespace Core.Dtos.Account;

public class GetProfileDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string[] Roles { get; set; } // бажано зробити enum
    public string AvatarPath { get; set; }
}
