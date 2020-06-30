using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Api.data;
using Api.models;
using AuthenticationPlugin;
using ImageUploader;
using Microsoft.AspNetCore.Authorization;
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
                user_id = userEmail.Id
            });

        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword([FromBody] ChangePassword changePassword)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = _dataContext.UserTbl.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }

            //ita passe dena old password ekai user eke tina password eki same da kiyala balanawa. 
            if (!SecurePasswordHasherHelper.Verify(changePassword.OldPassword, user.Password))
            {
                return Unauthorized("Sorry you can't change the password");
                //login wenakota tinan ekama change karala ganna puluwan
            }

            //ita passe body eke tina new password eka user ge password eka witdihata add wenna one.
            user.Password = SecurePasswordHasherHelper.Hash(changePassword.NewPassword);
            _dataContext.SaveChanges();
            return Ok("your password has been changed!");

        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangePhoneNumber([FromBody] ChangePhoneNumber changePhoneNumber)
        {
            //claim karala token eken ganna one
            var userEmail = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value;
            //data base eka email eka tyeda balanawa
            var user = _dataContext.UserTbl.FirstOrDefault(u => u.Email == userEmail);
            //balana conditon eka. 
            if (user == null)
            {
                return NotFound();
            }

            user.Phone = changePhoneNumber.ChangePhone;
            _dataContext.SaveChanges();
            return Ok("Phone Number Changed Successfully!");

        }

        [HttpPost]
        [Authorize]
        public IActionResult EditUserProfile([FromBody] byte[] ImageArray)
        {
            //claim karala token eken ganna one
            var userEmail = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value;
            //data base eka email eka tyeda balanawa
            var user = _dataContext.UserTbl.FirstOrDefault(u => u.Email == userEmail);
            //balana conditon eka. 
            if (user == null)
            {
                return NotFound();
            }

            //image eka upload karanna tinne
            var stream = new MemoryStream(ImageArray);
            var guid = Guid.NewGuid().ToString();
            var file = $"{guid}.jpg";
            var folder = "wwwroot";
            var response = FilesHelper.UploadImage(stream, folder, file);
            if (!response)
            {
                return BadRequest();
            }
            else
            {
                // ita passe karanna tinne  image eka save karana eka. 
                user.ImageURL=file;
                _dataContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

    }

}