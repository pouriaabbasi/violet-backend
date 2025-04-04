using violet.backend.Enums;

namespace violet.backend.Models.Auth;

public class CurrentUserModel
{
    public Guid Id { get; set; }
    public GenderType? Gender { get; set; }
}