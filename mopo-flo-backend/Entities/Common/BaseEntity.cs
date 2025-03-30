using System.ComponentModel.DataAnnotations;

namespace violet.backend.Entities.Common;

public class BaseEntity
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreateDate { get; set; } = DateTime.Now;

}