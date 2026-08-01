using Microsoft.AspNetCore.Http;

namespace Core.Dtos.Account;

public class UpdateProfileDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public IFormFile? Avatar { get; set; } // якщо аватарка та сама(або відсутня) = null, інакше - новий файл
}
