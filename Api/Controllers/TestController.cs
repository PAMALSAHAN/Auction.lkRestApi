namespace Api.Controllers
{
    using System.Collections.Generic;
    using Api.models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]

    public class TestController : Controller
    {
        // public IActionResult Index()
        // {
        //     return View();
        // }

        // public IActionResult Welcome()
        // {
        //     ViewData["Message"] = "Your welcome message";

        //     return View();
        // }
        static List<Vehicle> vehicleList=new List<Vehicle>(){
        new Vehicle(){Id=0,Price=100000,Title="wagonR"},
        new Vehicle(){Id=1,Price=200000,Title="apache"},
        new Vehicle(){Id=0,Price=300000,Title="scooty"},
        };

        [HttpGet] 
        public IEnumerable<Vehicle> Get(){
            return vehicleList;
        }

        [HttpPost]
        public void post([FromBody]Vehicle vehicle)
        {
            vehicleList.Add(vehicle);
            //vehicle list eka static karanawa ethakota eka copy ekai hadenne.
        }

        [HttpPut("{i}")]
        public void putvehicle(int i,[FromBody]Vehicle vehicle)
        {
            vehicleList[i]=vehicle;
            //vehicle list eka static karanawa ethakota eka copy ekai hadenne.
        }

        [HttpDelete("{i}")]
        public void Delete(int i)
        {
            vehicleList.RemoveAt(i);
            //vehicle list eka static karanawa ethakota eka copy ekai hadenne.
        }

        
            
            

            
    }
}
