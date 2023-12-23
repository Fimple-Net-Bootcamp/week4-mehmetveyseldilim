using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VirtualPetCare.Data.Contracts;
using VirtualPetCare.Data.DTOs;
using VirtualPetCare.Data.Entities;
using VirtualPetCare.Data.Entities.ConfigurationModels;
using VirtualPetCare.Data.Exceptions;

namespace VirtualPetCare.Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IOptionsMonitor<JwtConfiguration> _configuration;
        private readonly RoleManager<CustomIdentityRole> _roleManager;
        private readonly JwtConfiguration _jwtConfiguration;

        private User? _user;

        public AuthenticationRepository(IMapper mapper,
        UserManager<User> userManager, IOptionsMonitor<JwtConfiguration> configuration, RoleManager<CustomIdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _jwtConfiguration = _configuration.CurrentValue;

        }

        public async Task<UserForRead> GetUserByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if(user == null) 
            {
                throw new UserNotFound(id.ToString());
            }

            var roles = await _userManager.GetRolesAsync(user);

            var mappedUser = _mapper.Map<UserForRead>(user);
            mappedUser.Roles = roles;

            return mappedUser;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            await CheckIfRoleExists(userForRegistration.Roles);


            var result = await _userManager.CreateAsync(user, userForRegistration.Password!);

            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return result;
        }

        private async Task CheckIfRoleExists(ICollection<string> roles)
        {

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    throw new RoleNotFound(role);
                }
            }

        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            string? userName = userForAuth.UserName;

            if(userName == null) return false;

            string? password = userForAuth.Password;

            if(password == null) return false;

            _user = await _userManager.FindByNameAsync(userName);

            var result = _user != null && await _userManager.CheckPasswordAsync(_user, password);

            if (!result)
                Console.WriteLine($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");

            return result;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken();

            _user!.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto(accessToken, refreshToken);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRETTWO")!);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user!.UserName!),
                new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {

            Console.WriteLine($"liveLoading value : {_jwtConfiguration.liveLoading}");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);


            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            Console.WriteLine(Environment.GetEnvironmentVariable("SecretThree")!);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SecretThree")!)),
                ValidateLifetime = true,
                ValidIssuer = _jwtConfiguration.ValidIssuer,
                ValidAudience = _jwtConfiguration.ValidAudience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

            var user = await _userManager.FindByNameAsync(principal.Identity!.Name!);

            if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new RefreshTokenBadRequest();

            _user = user;
            return await CreateToken(populateExp: false);
        }


    }
}