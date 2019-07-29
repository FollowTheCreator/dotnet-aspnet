using ITechart.DotNet.AspNet.CustomModelBinder.Infrastructure.Binders;
using ITechart.DotNet.AspNet.CustomModelBinder.Models;
using ITechart.DotNet.AspNet.CustomModelBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITechart.DotNet.AspNet.CustomModelBinder.Controllers
{
    [Route("api/location")]
    public class LocationController : Controller
    {
        public ActionResult GetPoint([ModelBinder(Name = "coordinates")]Point point)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json(point);
        }

        [Route("{users-nearby}")]
        public ActionResult GetUsersNearby([ModelBinder(Name = "point")]Point point, int radius)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json(new
            {
                PointList = new List<Point> { point },
                Radius = radius
            });
        }
    }
}