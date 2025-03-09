using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;

namespace ServerLibrary.Repositories.Implementations
{
    public class UserAccountRepository(IOptions<JwtSection> config, AppDbContext appDbContext) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user is null) return new GeneralResponse(false, "Modelo está vazio");

            var checkUser = await FindUserByEmail(user.EmailAddress!);
            if (checkUser != null) return new GeneralResponse(false, "Usuário já registrado");

            var applicationUser = new ApplicationUser();

            applicationUser.FullnameFill(user.Fullname!);
            applicationUser.EmailFill(user.EmailAddress);
            applicationUser.PasswordFill(BCrypt.Net.BCrypt.HashPassword(user.Password));

            await AddToDatabase(applicationUser);

            // checar, criar e dar um role
            var checkAdminRole = await appDbContext.SystemRoles.First
        }

        public Task<LoginResponse> CreateAsync(Login user)
        {
            throw new NotImplementedException();
        }

        private async Task<ApplicationUser> FindUserByEmail(string email) =>
            await appDbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Email!.ToLower()!.Equals(email!.ToLower()));

        private async Task<T> AddToDatabase<T> (T model)
        {
            var result = appDbContext.Add(model!);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }
    }
}
