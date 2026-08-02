using Domain.Entities.Base;
using Domain.Entities.User;

namespace Domain.Entities;

public class City : BaseEntityWithIsDeleted<int>
{
    public int RegionId { get; set; }
    public string Name { get; set; }
    public string CenterPosition { get; set; }
    public double Radius { get; set; }
    public int MaxCourierCount { get; set; }
    public TimeOnly WorktimeStart { get; set; } = new TimeOnly(0, 0);
    public TimeOnly WorktimeEnd { get; set; } = new TimeOnly(23, 59, 59);

    // conn
    public Region Region { get; set; }
    public ICollection<CourierTimeSlot> CourierTimeSlots { get; set; }
    public ICollection<UserLocation> UserLocations { get; set; }
}
