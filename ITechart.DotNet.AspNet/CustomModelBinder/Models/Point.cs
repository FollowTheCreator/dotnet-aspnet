using ITechart.DotNet.AspNet.CustomModelBinder.Infrastructure.Binders;
using Microsoft.AspNetCore.Mvc;

namespace ITechart.DotNet.AspNet.CustomModelBinder.Models
{
    [ModelBinder(BinderType = typeof(PointModelBinder))]
    public class Point
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }
    }
}
