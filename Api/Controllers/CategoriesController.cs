using Api.data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CategoriesController(DataContext dataContext)
        {
            this._dataContext = dataContext;

        }

        [HttpGet]
        public IActionResult GetAction(){
            var categoryList=_dataContext.CategoryTbl;
            return Ok(categoryList);
        }

    }
}

