using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Api.data;
using Api.models;
using ImageUploader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ImagesController(DataContext dataContext)
        {
            this._dataContext = dataContext;

        }

        [HttpPost]
        [Authorize]
        public IActionResult post([FromBody] Image image)
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
            var stream = new MemoryStream(image.ImageArray);
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
                var imageOne=new Image(){
                    ImageURL=file,
                    VehicleId=image.VehicleId // from body eka thule yawana id eka.
                };
            
                _dataContext.ImageTbl.Add(imageOne);
                _dataContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

    }
}