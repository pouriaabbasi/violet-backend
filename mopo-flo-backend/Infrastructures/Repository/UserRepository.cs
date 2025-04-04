using Microsoft.EntityFrameworkCore;
using violet.backend.Entities;
using violet.backend.Enums;
using violet.backend.Events;
using violet.backend.Models.Auth;

namespace violet.backend.Infrastructures.Repository;

public interface IUserRepository : IUserBaseRepository
{
    Task<User> GetUserFromTelegramId(long telegramId);
    Task<User> UpdateTelegramInfo(User userEntity, TelegramInfoDto telegramInfoDto);
    Task CreateNewUser(User userEntity);
    Task<GenderType?> GetUserGender(Guid userId);
    Task ConvertUserToDerivedUser(Guid userId, GenderType gender);
    Task<User> GetUserFromUserId(Guid userId);
}

public class UserRepository(AppDbContext dbContext) : UserBaseRepository(dbContext), IUserRepository
{
    public async Task<User> GetUserFromTelegramId(long telegramId)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x =>
            x.TelegramInfo != null
            && x.TelegramInfo.TelegramId == telegramId);

        if (user == null) return null;

        return user;
        //return LoadUserFromEvents(user.Id);
    }

    public async Task<User> UpdateTelegramInfo(User userEntity, TelegramInfoDto telegramInfoDto)
    {
        userEntity.UpdateTelegramInfo(telegramInfoDto);
        dbContext.Users.Update(userEntity);
        await dbContext.SaveChangesAsync();

        await CreateEvent<UpdateTelegramInfoDomainEvent, TelegramInfoDto>(userEntity.Id, telegramInfoDto);

        return userEntity;
    }

    public async Task CreateNewUser(User userEntity)
    {
        await dbContext.AddAsync(userEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<GenderType?> GetUserGender(Guid userId)
    {
        bool isFemaleUser = await dbContext.FemaleUsers.AnyAsync(x => x.Id == userId);
        if (isFemaleUser) return GenderType.Female;

        bool isMaleUser = await dbContext.MaleUsers.AnyAsync(x => x.Id == userId);
        if (isMaleUser) return GenderType.Male;

        return null;
    }

    public async Task ConvertUserToDerivedUser(Guid userId, GenderType gender)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new Exception("UserId is not valid");

        switch (gender)
        {
            case GenderType.Female:
                await ConvertUserToFemaleUser(user);
                break;
            case GenderType.Male:
                await ConvertUserToMaleUser(user);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
        }
    }

    public async Task<User> GetUserFromUserId(Guid userId)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new Exception("UserId is not valid");

        return user;
    }

    private async Task ConvertUserToMaleUser(User user)
    {
        dbContext.Users.Remove(user);

        var maleUser = new MaleUser(user);
        await dbContext.MaleUsers.AddAsync(maleUser);
        await dbContext.SaveChangesAsync();
    }

    private async Task ConvertUserToFemaleUser(User user)
    {
        dbContext.Users.Remove(user);

        var femaleUser = new FemaleUser(user);
        await dbContext.FemaleUsers.AddAsync(femaleUser);
        await dbContext.SaveChangesAsync();
    }
}