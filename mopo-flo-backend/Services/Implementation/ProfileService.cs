using Microsoft.EntityFrameworkCore;
using violet.backend.Entities;
using violet.backend.Infrastructures;
using violet.backend.Models.Profile;
using violet.backend.Services.Contracts;

namespace violet.backend.Services.Implementation;

public class ProfileService(
    AppDbContext appDbContext,
    ICurrentUserService currentUserService) : IProfileService
{
    public Task<bool> HasProfile()
    {
        //return appDbContext.Profiles.AnyAsync(x => x.TelegramUserId == currentUserService.User.Id);
        return Task.FromResult(false);
    }

    public Task<ProfileDto> GetProfile()
    {
        //return appDbContext.Profiles
        //    //.Where(x => x.TelegramUserId == currentUserService.User.Id)
        //    .Select(x => new ProfileModel(x.Id, x.Name, x.Age, x.IsNewInPeriod, x.PeriodCycleDuration, x.BleedingDuration, x.Gender))
        //    .FirstOrDefaultAsync();

        return new Task<ProfileDto>(null);
    }

    public async Task<bool> UpdateProfile(UpdateProfileRequest request)
    {
        //var entity =
        //    await appDbContext.Profiles.FirstOrDefaultAsync();
        //if (entity == null)
        //{
        //    entity = new Profile
        //    {
        //        //TelegramUserId = currentUserService.User.Id
        //    };
        //    await appDbContext.Profiles.AddAsync(entity);
        //}

        //entity.Age = request.Age;
        //entity.IsNewInPeriod = request.IsNewInPeriod;
        //entity.Name = request.Name;
        //entity.PeriodCycleDuration = request.PeriodCycleDuration;
        //entity.BleedingDuration = request.BleedingDuration;
        //entity.Gender = request.Gender;

        //await appDbContext.SaveChangesAsync();

        return true;
    }
}