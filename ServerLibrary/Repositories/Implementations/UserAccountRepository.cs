using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;

using Constants = ServerLibrary.Helpers.Contansts;

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
            applicationUser.EmailFill(user.EmailAddress!);
            applicationUser.PasswordFill(BCrypt.Net.BCrypt.HashPassword(user.Password));

            await AddToDatabase(applicationUser);

            // checar, criar e dar um role
            var checkAdminRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Admin));
            if (checkAdminRole is null)
            {
                var createAdminRole = await AddToDatabase(new SystemRole() { Name = Constants.Admin });
                await AddToDatabase(new UserRole() { RoleId = createAdminRole.Id, UserId = applicationUser.Id });

                return new GeneralResponse(true, "Conta criada");
            }

            var checkUserRole = await appDbContext.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Contansts.User));
            SystemRole response = new();
            if (checkUserRole is null)
            {
                response = await AddToDatabase(new SystemRole() { Name = Constants.User });
                await AddToDatabase(new UserRole() { RoleId = response.Id, UserId = applicationUser.Id });
            }
            else
                await AddToDatabase(new UserRole() { RoleId = checkUserRole.Id, UserId = applicationUser.Id });

            return new GeneralResponse(true, "Conta criada");
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
