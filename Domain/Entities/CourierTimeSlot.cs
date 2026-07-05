using Core.Entities.Identity;

namespace Domain.Entities;

public class CourierTimeSlot
{
    public int Id { get; set; }
    public Guid UserId { get; set; } // null?
    public DateTime WorktimeStart { get; set; }
    public DateTime WorktimeEnd { get; set; }
    public int CityId { get; set; }

    // conn
    public UserEntity User { get; set; }
    public City City { get; set; }
}
