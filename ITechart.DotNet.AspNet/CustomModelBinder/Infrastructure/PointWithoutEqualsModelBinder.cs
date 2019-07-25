using ITechart.DotNet.AspNet.CustomModelBinder.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ITechart.DotNet.AspNet.CustomModelBinder.Infrastructure
{
    public class PointWithoutEqualsModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var value = bindingContext
                .HttpContext
                .Request
                .QueryString
                .Value;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            var coordsArray = value.Replace("?coordinates", string.Empty).Split(',');

            if (coordsArray.Count() != 3)
            {
                return Task.CompletedTask;
            }

            int[] coords = coordsArray
                .Select(coord =>
                {
                    if (String.IsNullOrEmpty(coord))
                    {
                        return 0;
                    }

                    return int.Parse(coord);
                })
                .ToArray();

            bindingContext.Result = ModelBindingResult.Success(
                new Point
                {
                    X = coords[0],
                    Y = coords[1],
                    Z = coords[2]
                }
            );

            return Task.CompletedTask;
        }
    }
}
