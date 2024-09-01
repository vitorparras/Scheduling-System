﻿using Application.Services.Interfaces;
using Domain.DTO;
using Infrastructure.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenHistoryRepository _tokenHistoryRepository;


        public JwtService(
            IConfiguration configuration,
            ITokenHistoryRepository tokenHistoryRepository)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _tokenHistoryRepository = tokenHistoryRepository ?? throw new ArgumentNullException(nameof(tokenHistoryRepository));
        }

        public GenericResponse<string> GenerateJwtToken(UserDTO user)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email)
                    ]),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new GenericResponse<string>(tokenHandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while we try to generate jwt token", ex);
            }
        }

        public async Task<GenericResponse<bool>> TokenIsValid(string token)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidate = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                });

                var tokenValid = await _tokenHistoryRepository.IsTokenValidAsync(token);
                if (!tokenValid.Success) return new GenericResponse<bool>("Token is not valid", false);

                return new GenericResponse<bool>(tokenValidate.IsValid && tokenValid.Data);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while we try to generate jwt token", ex);
            }
        }
    }
}