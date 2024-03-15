using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using SibSIU.UserData.Database.Entities;

using System;

namespace SibSIU.Auth.Database.Extensions;
public static class GenderExtensions
{
    public static async Task<Gender?> GetGenderById(this DbSet<Gender> genders,
        IMemoryCache memory,
        Ulid id, CancellationToken cancellationToken)
    {
        string cacheKey = $"gender_{id}";
        if (!memory.TryGetValue(cacheKey, out Gender? gender))
        {
            gender = await genders.Where(g => g.Id == id).SingleOrDefaultAsync(cancellationToken);

            MemoryCacheEntryOptions options = new()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(30),
            };

            memory.Set(cacheKey, gender, options);
        }

        return gender;
    }
}
