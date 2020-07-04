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
        public IActionResult Post(Vehicle vehicle)
        {
            //auth hinda token eken email eka ganna one
            var userEmail = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value;
            //data base eka email eka tyeda balanawa
            var user = _dataContext.UserTbl.FirstOrDefault(u => u.Email == userEmail);


            //balana conditon eka. 
            if (user == null)
            {
                return NotFound();
            }




            var vehicleObj = new Vehicle()
            {
                Title = vehicle.Title,
                Description = vehicle.Title,
                Color = vehicle.Color,
                Company = vehicle.Company,
                Condition = vehicle.Condition,
                DatePosted = vehicle.DatePosted,
                Engine = vehicle.Engine,
                Price = vehicle.Price,
                Model = vehicle.Model,
                Location = vehicle.Location,
                CategoryId = vehicle.CategoryId,
                IsFeatured = false,
                IsHotandNew = false,
                UserId = user.Id
            };


            _dataContext.vehiclesTbl.Add(vehicleObj);
            _dataContext.SaveChanges();
            return Ok(new { vehicleId = vehicleObj.Id, message = "vehicle added successfully" });
            //methana karanne vehile images danawane ewwata id eka return karanawa.

        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult HotAndNewAds()
        {
            var vehicles = from v in _dataContext.vehiclesTbl
                            where v.IsHotandNew == true
                            select new
                            {
                                Id = v.Id,
                                Title = v.Title,
                                ImageUrl = v.Images.FirstOrDefault().ImageURL
                            };
            return Ok(vehicles);


        }

        [HttpGet("[action]")]
        [Authorize]
        public IActionResult SearchVehicle(string search)
        {
            var vehicles = from v in _dataContext.vehiclesTbl
                            where v.Title.StartsWith(search)
                            select new
                            {
                                Id = v.Id,
                                Title = v.Title,
                            
                            };
            return Ok(vehicles);

        }

        [HttpGet]
        [Authorize]
         public IActionResult getVehicle(int categoryId)
        {
            var vehicles = from v in _dataContext.vehiclesTbl
                            where v.CategoryId==categoryId
                            select new
                            {
                                Id = v.Id,
                                Title = v.Title,
                                Price =v.Price,
                                Location =v.Location,
                                ImageUrl =v.Images.FirstOrDefault().ImageURL,
                                DatePosted =v.DatePosted,
                                IsFeatured =v.IsFeatured
                            
                            };
            return Ok(vehicles);

        }

    }
}