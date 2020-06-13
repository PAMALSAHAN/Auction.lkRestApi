
using System.Collections.Generic;
using Api.data;
using Api.models;
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
        public IEnumerable<Vehicle> Get()
        {
            return _dataContext.vehiclesTbl;
        }

        // GET: api/ApiReadWrite/5
        [HttpGet("{id}")]
        public Vehicle Get(int id)
        {
            var vehicle=_dataContext.vehiclesTbl.Find(id);
            return vehicle;
        }

        // POST: api/ApiReadWrite
        [HttpPost]
        public void Post([FromBody] Vehicle vehicle)
        {
            _dataContext.vehiclesTbl.Add(vehicle);
            _dataContext.SaveChanges();
        }

        // PUT: api/ApiReadWrite/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Vehicle vehicle)
        {
            var entity =_dataContext.vehiclesTbl.Find(id);
            entity.Title=vehicle.Title ?? entity.Title;
            entity.Price=vehicle.Price ?? entity.Price;  //methana vehicle eke double danna baha eka hinda athana ? eka use karanawa. 
        
            _dataContext.SaveChanges();

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var entity=_dataContext.vehiclesTbl.Find(id);
            _dataContext.vehiclesTbl.Remove(entity);
            _dataContext.SaveChanges();
        }
    }
}