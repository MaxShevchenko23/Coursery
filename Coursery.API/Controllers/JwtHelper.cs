using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Coursery.Application.Dtos.Get;
using Coursery.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace CourseryPL.Controllers.CourseryPL.Controllers;

public static class JwtHelper
{
    public static string? GenerateToken(GetUserDto user)
    {
        if (user == null)
        {
            return null;
        }
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretnotsosecretsecretnotsosecretsecretnotsosecretsecretnotsosecretsecretnotsosecret"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("firstName", user.FirstName),
            new Claim("lastName", user.LastName),
            new Claim("role",user.Role.ToString()),
            new Claim("profession", user.Profession ?? string.Empty),
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(300),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public static GetUserDto? DecodeToken(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        if (token.Contains("Bearer "))
        {
            token = token.Replace("Bearer ", "");
        }
        
        Console.WriteLine(token);
        
        if (string.IsNullOrEmpty(token))
        {
            throw new SecurityTokenMalformedException("Token is null or empty.");
        }

        var handler = new JwtSecurityTokenHandler();

        if (!handler.CanReadToken(token))
        {
            throw new SecurityTokenMalformedException("JWT is not well formed. Token is - " + token);
        }

        var jwtToken = handler.ReadJwtToken(token);
    
        var id = int.Parse(jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);
        var email = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Email).Value;
        var firstName = jwtToken.Claims.First(claim => claim.Type == "firstName").Value;
        var lastName = jwtToken.Claims.First(claim => claim.Type == "lastName").Value;
        var profession = jwtToken.Claims.First(claim => claim.Type == "profession").Value;
        var role = (Role)Enum.Parse(typeof(Role), jwtToken.Claims.First(claim => claim.Type == "role").Value);
    
        return new GetUserDto
        {
            Id = id,
            Email = email,
            LastName = lastName,
            FirstName = firstName,
            Role = role,
            Profession = profession,
        };
    }
}