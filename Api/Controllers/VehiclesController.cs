using System.Linq;
using System.Security.Claims;
using Api.data;
using Api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public VehiclesController(DataContext dataContext)
        {
            this._dataContext = dataContext;

        }

        [HttpPost]
        [Authorize]
        public IActionResult Post(Vehicle vehicle){
            //auth hinda token eken email eka ganna one
             var userEmail = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value;
            //data base eka email eka tyeda balanawa
            var user = _dataContext.UserTbl.FirstOrDefault(u => u.Email == userEmail);


            //balana conditon eka. 
            if (user == null)
            {
                return NotFound();
            }

          
        

            var vehicleObj=new Vehicle(){
                Title=vehicle.Title,
                Description=vehicle.Title,
                Color=vehicle.Color,
                Company=vehicle.Company,
                Condition=vehicle.Condition,
                DatePosted=vehicle.DatePosted,
                Engine=vehicle.Engine,
                Price=vehicle.Price,
                Model=vehicle.Model,
                Location=vehicle.Location,
                CategoryId=vehicle.CategoryId,
                IsFeatured=false,
                IsHotandNew=false,
                UserId=user.Id
            };
        

            _dataContext.vehiclesTbl.Add(vehicleObj);
            _dataContext.SaveChanges();
            return Ok(new {vehicleId=vehicleObj.Id,message="vehicle added successfully"});
            //methana karanne vehile images danawane ewwata id eka return karanawa.

        }
        
    }
}