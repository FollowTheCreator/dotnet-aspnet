using JsonFormatter.DAL.Repositories;
using JsonFormatter.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonFormatter.WebUI.Controllers
{
    [Route("api/getPerson")]
    public class PersonController : Controller
    {
        private readonly IProfileRepository _repository;

        public PersonController(IProfileRepository repository)
        {
            _repository = repository;
        }

        [Route("{id}")]
        public async Task<ActionResult<ResponseProfileModel>> Index(int id)
        {
            var dbResult = Utils.Convert.To<DAL.Models.Profile, ProfileModel>(await _repository.GetByIdAsync(id));

            var link = GetLink(id);
            
            var result = new ResponseProfileModel
            {
                Data = dbResult,
                Link = link
            };
            return result;
        }

        [NonAction]
        private string GetLink(int id)
        {
            var link = "http";
            if (Request.IsHttps)
            {
                link += "s";
            }
            link += $"://{Request.Host}/api/profile/{id}";

            return link;
        }
    }
}
