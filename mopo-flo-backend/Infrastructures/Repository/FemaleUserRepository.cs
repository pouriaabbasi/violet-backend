using Microsoft.EntityFrameworkCore;
using violet.backend.Entities;
using violet.backend.Events;
using violet.backend.Models.Profile;

namespace violet.backend.Infrastructures.Repository;

public interface IFemaleUserRepository : IUserBaseRepository
{
    Task<FemaleUser> GetUserFromUserId(Guid userId);
    Task UpdateProfile(FemaleUser user, UpdateProfileRequest request);
}

public class FemaleUserRepository(AppDbContext dbContext) : UserBaseRepository(dbContext), IFemaleUserRepository
{
    public Task<FemaleUser> GetUserFromUserId(Guid userId)
        => dbContext.FemaleUsers.FirstOrDefaultAsync(x => x.Id == userId);

    public async Task UpdateProfile(FemaleUser user, UpdateProfileRequest request)
    {
        user.UpdateProfile(request);

        await CreateEvent<UpdateProfileDomainEvent, UpdateProfileRequest>(user.Id, request);
    }
}