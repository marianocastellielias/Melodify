using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthenticationService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }


        private User? ValidateUser(AuthenticationValidate authenticationValidate)
        {
            if (string.IsNullOrEmpty(authenticationValidate.Email) || string.IsNullOrEmpty(authenticationValidate.Password))
                return null;

            var user = _userRepository.GetUserByEmail(authenticationValidate.Email);
            if (user == null) return null;
            if (user.Email == authenticationValidate.Email && user.Password == authenticationValidate.Password) return user;
            return null;
        }
        public string Authenticate(AuthenticationValidate authenticationRequest)
        {
            var user = ValidateUser(authenticationRequest);

            if (user == null)
            {
                //throw new Exception("User authentication failed");
                return null;
            }

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AutenticacionService:SecretForKey"] ?? ""));
            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

           
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.Id.ToString())); 
            claimsForToken.Add(new Claim("name", user.Name)); 
            claimsForToken.Add(new Claim("role", user.Role.ToString()));

            var jwtSecurityToken = new JwtSecurityToken( 
              _configuration["AutenticacionService:Issuer"],
              _configuration["AutenticacionService:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddHours(1),
              credentials);

            var tokenToReturn = new JwtSecurityTokenHandler() 
                .WriteToken(jwtSecurityToken);

            return tokenToReturn.ToString();
        }
    }
}
