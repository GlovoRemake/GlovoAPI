namespace Domain.Entities;

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CenterPosition { get; set; }

    // conn
    public ICollection<City>? Cities { get; set; }
}
