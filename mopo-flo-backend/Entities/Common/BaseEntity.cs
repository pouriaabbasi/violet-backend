using System.ComponentModel.DataAnnotations;

namespace mopo_flo_backend.Entities.Common;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
}