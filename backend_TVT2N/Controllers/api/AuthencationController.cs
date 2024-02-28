using backend_TVT2N.Models;
using Libs.Entity;
using Libs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend_TVT2N.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthencationController : ControllerBase
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private IConfiguration _configuration;
        private ProfileService profileService;

        public AuthencationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ProfileService profileService) { 
            this.userManager= userManager;
            this.roleManager = roleManager;
            this._configuration = configuration;
            this.profileService = profileService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Registeration(RegistrationModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return Ok(new { status = false, message = "already" });
            } else
            {
                IdentityUser user = new();
                user.UserName = model.Username;
                var createUserResult = await userManager.CreateAsync(user, model.Password);
                if (createUserResult.Succeeded)
                {
                    Profile profile = new Profile();
                    profile.Id = Guid.NewGuid();
                    profile.UserId = user.Id;
                    profile.Name = user.UserName;
                    profile.TypePayment = 1;
                    profileService.insertProfile(profile);
                    return Ok(new { status = true, message = "success"});
                }
                else
                {
                    return Ok(new { status = false, message = "error" });
                }
                
            }
            
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(RegistrationModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
                return Ok(new { status = false, message = "username" });
            if (!await userManager.CheckPasswordAsync(user, model.Password))
                return Ok(new { status = false, message = "password" });

            var userRoles = await userManager.GetRolesAsync(user);


            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim("Id", user.Id.ToString())
            };
            var roleIdentity = roleManager.Roles.Where(s => userRoles.Contains(s.Name)).ToList();
            //var claims = roleManager.GetClaimsAsync(roleIdentity).Result;
            foreach (var userRole in roleIdentity)
            {
                var claims = roleManager.GetClaimsAsync(userRole).Result;
                for (int i = 0; i < claims.Count; i++)
                {
                    authClaims.Add(claims[i]);
                }
                authClaims.Add(new Claim(ClaimTypes.Role, userRole.Name));
            }
            string token = GenerateToken(authClaims);
            return Ok(new { status = true, message = "", token = token });

        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWTKey:ValidIssuer"],
                Audience = _configuration["JWTKey:ValidAudience"],
                //Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
