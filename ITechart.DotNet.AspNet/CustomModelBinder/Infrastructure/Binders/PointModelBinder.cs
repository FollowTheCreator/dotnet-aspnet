using ITechart.DotNet.AspNet.CustomModelBinder.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ITechart.DotNet.AspNet.CustomModelBinder.Infrastructure.Binders
{
    public class PointModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var value = bindingContext
                .HttpContext
                .Request
                .QueryString
                .Value;

            if (string.IsNullOrEmpty(value) || !value.Contains(modelName))
            {
                return Task.CompletedTask;
            }

            var coordsArray = value.Replace($"?{modelName}", string.Empty).Split(',');

            if (coordsArray.Count() != 3)
            {
                return Task.CompletedTask;
            }

            coordsArray[0] = coordsArray[0].Replace("=", string.Empty);

            int coord = 0;
            int[] coords = coordsArray
                .Select(item =>
                {
                    int.TryParse(item, out coord);

                    return coord;
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
