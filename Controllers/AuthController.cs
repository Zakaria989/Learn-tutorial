using Azure.Core;
using LearnTutorial.DTOs;
using LearnTutorial.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;



namespace LearnTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public string DbManager { get; set; }
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            //DbManager = _configuration.GetSection("AppSettings:DeafultConnection").Value!;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDTO request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            User user = new User
            {
                PasswordHash = passwordHash,
                UserName = request.Username
            };

            try
            {
                using (SqlConnection con = DatabaseManager.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("uspInsertUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@userName", user.UserName));
                    cmd.Parameters.Add(new SqlParameter("@passwordHash", user.PasswordHash));

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserDTO request)
        {
            User user = new User 
            {
                UserName = request.Username
            };
            //CheckUser method checks for the username and password
            user.CheckUser();
            if (user.UserName == null)
            {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string jwt = CreateToken(user);
            return Ok(jwt);
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            return new SymmetricSecurityKey(key);
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var key = GetSymmetricSecurityKey();
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
