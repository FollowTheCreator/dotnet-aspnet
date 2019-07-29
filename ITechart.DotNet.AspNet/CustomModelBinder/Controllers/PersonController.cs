using System;
using System.Linq;
using System.Threading.Tasks;
using ITechart.DotNet.AspNet.CustomModelBuilder.Models.Contexts;
using ITechart.DotNet.AspNet.CustomModelBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITechart.DotNet.AspNet.CustomModelBinder.Infrastructure.Binders;

namespace ITechart.DotNet.AspNet.CustomModelBinder.Controllers
{
    [Route("api/person")]
    public class PersonController : Controller
    {
        private readonly PeopleDbContext _context;

        public PersonController(PeopleDbContext context)
        {
            _context = context;
        }

        [Route("{id}")]
        public async Task<ActionResult> GetPersonById([ModelBinder(BinderType = typeof(PersonModelBinder), Name = "id")]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json(await _context.People.Where(item => item.Id == id).FirstOrDefaultAsync());
        }
    }
}