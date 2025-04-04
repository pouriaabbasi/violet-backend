using Microsoft.EntityFrameworkCore;
using violet.backend.Entities;
using violet.backend.Events;
using violet.backend.Models.Profile;

namespace violet.backend.Infrastructures.Repository;

public interface IMaleUserRepository : IUserBaseRepository
{
    Task<MaleUser> GetUserFromUserId(Guid userId);
    Task UpdateProfile(MaleUser user, UpdateProfileRequest request);
}

public class MaleUserRepository(AppDbContext dbContext) : UserBaseRepository(dbContext), IMaleUserRepository
{
    public Task<MaleUser> GetUserFromUserId(Guid userId)
        => dbContext.MaleUsers.FirstOrDefaultAsync(x => x.Id == userId);

    public async Task UpdateProfile(MaleUser user, UpdateProfileRequest request)
    {
        user.UpdateProfile(request);

        await CreateEvent<UpdateProfileDomainEvent, UpdateProfileRequest>(user.Id, request);
    }
}