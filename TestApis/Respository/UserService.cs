using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestApis.Contract;
using TestApis.Model;

namespace TestApis.Respository
{
    public class UserService : IUsers
    {
        private ApplicationContext context;
        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings,ApplicationContext application)
        {
            context = application;
            _appSettings = appSettings.Value;
        }

        public dynamic Signin(LoginModel model)
        {
            var user = context.Users.SingleOrDefault(e => e.Email == model.Email && e.Password == model.Password);
            if (user != null)
            {
                return new ApiResponse() { Message = "sucess", Data = user, Status = 200,Token=generateJwtToken(user) };
            }
            else
            {
                return new ApiResponse() { Message = "invalid credential !", Status = 404 };
            }
        }

        private string generateJwtToken(Users user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public dynamic SignUp(Users user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return new ApiResponse() { Message = "Created !", Status = 201 };
        }
    }
}
