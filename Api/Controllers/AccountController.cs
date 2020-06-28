using System.Linq;
using Api.data;
using Api.models;
using AuthenticationPlugin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public AccountController(DataContext dataContext)
        {
            this._dataContext = dataContext;

        }


        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            var sameEmail=_dataContext.UserTbl.Where(u => u.Email == user.Email).SingleOrDefault();
            if (sameEmail!=null)
            {
                return BadRequest("Email already used!!");
            }
            else{

                var userObj=new User(){

                    Name=user.Name,
                    Email=user.Email,
                    Password=SecurePasswordHasherHelper.Hash(user.Password)
                };

                _dataContext.UserTbl.Add(userObj);
                _dataContext.SaveChanges();

                return StatusCode(StatusCodes.Status201Created);
            }
        }

    }

}