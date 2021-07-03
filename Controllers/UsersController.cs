using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using authApi.Data;
using authApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace authApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly authApiContext _context;
        private readonly IDataRepository<User> _repo;
        private IConfiguration _config;

        public class Login
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        public UsersController(authApiContext context, IDataRepository<User> repo, IConfiguration config)
        {
            _context = context;
            _repo = repo;
            _config = config;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _context.User.OrderByDescending(p => p.id);
        }
        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("/api/verify/{token}")]
        public async Task<IActionResult> VerifyToken([FromRoute] string token)
        {
            var userToken = (from u in _context.User
                             where u.token == token
                             select u).Single();
            if (userToken != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] Guid id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id)
            {
                return BadRequest();
            }

            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Registers
        [HttpPost("/api/Registers")]
        public async Task<ActionResult<User>> PostRegister(User user)
        {
            string email = user.email;
            string password = user.password;

            if (UserExists(email))
            {
                return BadRequest(user);
            }
            else
            {
                user.password = BCrypt.Net.BCrypt.HashPassword(password);
                _context.User.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = user.id }, user);
            }
        }


        // POST: api/Login
        [HttpPost("/api/Login")]
        public async Task<ActionResult<User>> PostLogin([FromBody] Login loginInfo)
        {
            string email = loginInfo.email;
            string password = loginInfo.password;

            if (UserExists(email))
            {
                var user = (from d in _context.User where d.email == email select d).Single();
                if (isPasswordCorrect(password,user.password))
                {
                    var tokenString = GenerateJSONWebToken(user);
                    return Ok(new { token = tokenString });
                }
                else
                    return Unauthorized();
            }
            else
                return Unauthorized();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.User.FindAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
        private bool UserExists(Guid id)
        {
            return _context.User.Any(e => e.id == id);
        }

        private bool UserExists(string email)
        {
            return _context.User.Any(e => e.email == email);
        }

        private bool isPasswordCorrect(string userEnteredPassword, string hashedPwdFromDatabase)
        {
           return BCrypt.Net.BCrypt.Verify(userEnteredPassword, hashedPwdFromDatabase);
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Issuer"], null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
