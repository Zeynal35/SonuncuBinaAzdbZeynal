using Application.Options;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Persistence.Data;

public static class AdminSeeder
{
    public static async Task SeedAsync(
        UserManager<User> userManager,
        IOptions<SeedOptions> seedOptions)
    {
        var opt = seedOptions.Value;

        if (string.IsNullOrWhiteSpace(opt.AdminEmail) ||
            string.IsNullOrWhiteSpace(opt.AdminPassword))
            return;

        var exists = await userManager.FindByEmailAsync(opt.AdminEmail);
        if (exists is not null)
            return;

        var admin = new User
        {
            UserName = opt.AdminEmail,
            Email = opt.AdminEmail,
            FullName = opt.AdminFullName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, opt.AdminPassword);
        if (result.Succeeded)
            await userManager.AddToRoleAsync(admin, RoleNames.Admin);
    }
}

