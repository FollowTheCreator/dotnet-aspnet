using ITechart.DotNet.AspNet.CustomModelBinder.Infrastructure;
using ITechart.DotNet.AspNet.CustomModelBinder.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITechart.DotNet.AspNet.CustomModelBinder.Controllers
{
    [Route("api/location")]
    public class LocationController : Controller
    {
        public ActionResult<Point> GetPoint([ModelBinder(typeof(PointModelBinder))]Point point)
        {
            return point;
        }
    }
}