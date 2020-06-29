using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Api.data;
using Api.models;
using AuthenticationPlugin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private IConfiguration _configuration;
        private readonly AuthService _auth;

        public AccountController(DataContext dataContext, IConfiguration configuration)
        {
            this._dataContext = dataContext;
            _configuration = configuration;
            _auth = new AuthService(_configuration);

        }


        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            var sameEmail = _dataContext.UserTbl.Where(u => u.Email == user.Email).SingleOrDefault();
            if (sameEmail != null)
            {
                return BadRequest("Email already used!!");
            }
            else
            {

                var userObj = new User()
                {

                    Name = user.Name,
                    Email = user.Email,
                    Password = SecurePasswordHasherHelper.Hash(user.Password)
                };

                _dataContext.UserTbl.Add(userObj);
                _dataContext.SaveChanges();

                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            var userEmail = _dataContext.UserTbl.FirstOrDefault(u => u.Email == user.Email);
            if (userEmail == null)
            {
                return NotFound();
            }
            //password eka check karana eka
            if (!SecurePasswordHasherHelper.Verify(user.Password, userEmail.Password))
            {
                return Unauthorized();
                //methanadi not found newe unauthorized danna one mokada password wadradi kiyanne 
                //unauthorized kenek thami log wenne hinda. 

            }

            //jwt token eka thama danna one
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = _auth.GenerateAccessToken(claims);

            //json eke pennanna one tika pennanne meke me widihata
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                user_id=userEmail.Id 
            });

        }

    }

}