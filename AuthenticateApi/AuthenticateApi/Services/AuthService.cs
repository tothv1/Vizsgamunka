using AtuhenticateApi.Datas;
using AtuhenticateApi.Models;
using AtuhenticateApi.Models.Dtos;
using AtuhenticateApi.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AtuhenticateApi.Services
{
    public class AuthService : IAuth
    {
        private readonly AppDbContext dataBase;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public AuthService(AppDbContext dataBase, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.dataBase = dataBase;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = dataBase.AppUsers.FirstOrDefault(user => user.Email.ToLower() == email.ToLower());

            if (user != null)
            {
                if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //Itt készülnek a Role-ok
                    roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await userManager.AddToRoleAsync(user, roleName);

                return true;
            }

            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await dataBase.AppUsers.
                FirstOrDefaultAsync(user => user.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            var roles = await userManager.GetRolesAsync(user);
            var token = jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName
            };

            LoginResponseDto loginResponseDto = new()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegisterRequestDto registerRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registerRequestDto.UserName,
                NormalizedUserName = registerRequestDto.UserName.ToUpper(),
                Age = registerRequestDto.Age,
                FullName = registerRequestDto.FullName,
                Email = registerRequestDto.Email,

            };
            try
            {
                var result = await userManager.CreateAsync(user, registerRequestDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = dataBase.AppUsers.
                        First(user => user.UserName == registerRequestDto.UserName);


                    UserDto userDto = new()
                    {
                        Id = userToReturn.Id,
                        Email = userToReturn.Email,
                        UserName = userToReturn.UserName,
                        FullName = userToReturn.FullName,
                        Age = userToReturn.Age

                    };

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception e)
            {

            }

            return "Error Encountered!";
        }
    }
}
