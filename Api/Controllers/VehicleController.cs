
using System.Collections.Generic;
using Api.data;
using Api.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public VehicleController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        // GET: api/ApiReadWrite
        [HttpGet]
        public IActionResult Get()
        {
            // return _dataContext.vehiclesTbl;

            return Ok(_dataContext.vehiclesTbl);
            // return StatusCode(200);
            // return StatusCode(StatusCodes.Status200OK);

        }

        // GET: api/ApiReadWrite/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var vehicle = _dataContext.vehiclesTbl.Find(id);
            // return vehicle;
            if (vehicle==null)  
            {
                return NotFound("record not found");
            }
            else{
                return Ok("Record is on the database");
            }

        }

        // POST: api/ApiReadWrite
        [HttpPost]
        public IActionResult Post([FromBody] Vehicle vehicle)
        {
            _dataContext.vehiclesTbl.Add(vehicle);
            _dataContext.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);
        }

        // PUT: api/ApiReadWrite/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Vehicle vehicle)
        {
            var entity = _dataContext.vehiclesTbl.Find(id);
            if (entity == null)
            {
                return NotFound("record not found");
            }
            else
            {
                entity.Title = vehicle.Title ?? entity.Title;
                entity.Price = vehicle.Price ?? entity.Price;  //methana vehicle eke double danna baha eka hinda athana ? eka use karanawa. 
                entity.color=vehicle.color ?? entity.color;
                _dataContext.SaveChanges();

                return Ok("updated successfully");

            }


        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _dataContext.vehiclesTbl.Find(id);
            if (entity == null)
            {
                return NotFound("record is not found");

            }
            else
            {
                _dataContext.vehiclesTbl.Remove(entity);
                _dataContext.SaveChanges();
                return Ok("successfully added");
            }


        }
    }
}