using violet.backend.Entities.Common;

namespace violet.backend.Entities;

public class Profile : BaseEntity
{
    public string Name { get; set; }
    public int Age { get; set; }
}