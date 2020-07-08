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
                                //methana image url eka ganne images kiyana eka one to many ekak eka hinda karanna 
                                //one image ekak use karana eka ekata karanna tinne first on default 
                                ImageUrl =v.Images.FirstOrDefault().ImageURL,

                                DatePosted =v.DatePosted,
                                IsFeatured =v.IsFeatured
                            
                            };
            return Ok(vehicles);

        }

        [HttpGet("[action]")]
        [Authorize] 
        public IActionResult vehicleDetails(int id){

            var foundVehicle=_dataContext.vehiclesTbl.Find(id);
            if(foundVehicle == null){
                return NoContent();
            }
            var vehicle=from v in _dataContext.vehiclesTbl
            join u in _dataContext.UserTbl on v.UserId equals u.Id
            where v.Id==id
            select new{
                id=v.Id,
                title=v.Title,
                description=v.Description,
                price=v.Price,
                models=v.Model,
                engine=v.Engine,
                color=v.Color,
                company=v.Company,
                datePosted=v.DatePosted,
                condition=v.Condition,
                location=v.Location,
                images=v.Images,
                email=u.Email,
                phone=u.Phone,
                imageUrl=u.ImageURL
            };

            return Ok(vehicle);

        }

        [HttpGet("[action]")]
        [Authorize] 
        public IActionResult myAds(){

            //me function eken karanne man hadapu adds tika pennanawa.ekedi use karanawa token ekak. eken 
            //thama hariyatama log wechchha person hoyaganne.

             //auth hinda token eken email eka ganna one
            var userEmail = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value;
            //data base eka email eka tyeda balanawa
            var user = _dataContext.UserTbl.FirstOrDefault(u => u.Email == userEmail);


            //balana conditon eka. 
            if (user == null)
            {
                return NotFound();
            }


            var vehicle=from v in _dataContext.vehiclesTbl

            where v.UserId==user.Id
            select new{
                id=v.Id,
                title=v.Title,
                price=v.Price,
                models=v.Model,
                datePosted=v.DatePosted,
                location=v.Location,
                images=v.Images,
                imageUrl=v.Images.FirstOrDefault().ImageURL
            };

            return Ok(vehicle);

        }

        

    }
}