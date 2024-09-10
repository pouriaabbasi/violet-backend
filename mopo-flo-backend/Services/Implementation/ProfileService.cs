using Microsoft.EntityFrameworkCore;
using mopo_flo_backend.Entities;
using mopo_flo_backend.Infrastructures;
using mopo_flo_backend.Models.Profile;
using mopo_flo_backend.Services.Contracts;

namespace mopo_flo_backend.Services.Implementation;

public class ProfileService(
    AppDbContext appDbContext,
    ICurrentUserService currentUserService) : IProfileService
{
    public Task<bool> HasProfile()
    {
        return appDbContext.Profiles.AnyAsync(x => x.TelegramUserId == currentUserService.User.Id);
    }

    public Task<ProfileModel> GetProfile()
    {
        return appDbContext.Profiles
            .Where(x => x.TelegramUserId == currentUserService.User.Id)
            .Select(x => new ProfileModel(x.Id, x.Name, x.Age, x.IsNewInPeriod, x.PeriodCycleDuration, x.BleedingDuration, x.Gender))
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateProfile(UpdateProfileRequest request)
    {
        var entity =
            await appDbContext.Profiles.FirstOrDefaultAsync(x => x.TelegramUserId == currentUserService.User.Id);
        if (entity == null)
        {
            entity = new Profile
            {
                TelegramUserId = currentUserService.User.Id
            };
            await appDbContext.Profiles.AddAsync(entity);
        }

        entity.Age = request.Age;
        entity.IsNewInPeriod = request.IsNewInPeriod;
        entity.Name = request.Name;
        entity.PeriodCycleDuration = request.PeriodCycleDuration;
        entity.BleedingDuration = request.BleedingDuration;
        entity.Gender = request.Gender;

        await appDbContext.SaveChangesAsync();

        return true;
    }
}