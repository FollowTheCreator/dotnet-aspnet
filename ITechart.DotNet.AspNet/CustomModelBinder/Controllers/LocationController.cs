using ITechart.DotNet.AspNet.CustomModelBinder.Infrastructure.Binders;
using ITechart.DotNet.AspNet.CustomModelBinder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ITechart.DotNet.AspNet.CustomModelBinder.Controllers
{
    [Route("api/location")]
    public class LocationController : Controller
    {
        public ActionResult<Point> GetPoint([ModelBinder(BinderType = typeof(PointModelBinder), Name = "coordinates")]Point point)
        {
            return point;
        }
    }
}