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
                .ValueProvider
                .GetValue(modelName)
                .FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                bindingContext.ModelState.TryAddModelError(
                            modelName,
                            $"{modelName} must exist.");

                bindingContext.Result = ModelBindingResult.Failed();

                return Task.CompletedTask;
            }

            var coordsArray = value.Replace($"{modelName}", string.Empty).Split(',');

            if (coordsArray.Count() != 3)
            {
                bindingContext.ModelState.TryAddModelError(
                            modelName,
                            $"{modelName} must have 3 arguments.");

                bindingContext.Result = ModelBindingResult.Failed();

                return Task.CompletedTask;
            }

            int coord = 0;
            bool isSuccessfully = true;
            int[] coords = coordsArray
                .Select(item =>
                {
                    if(!int.TryParse(item, out coord))
                    {
                        bindingContext.ModelState.TryAddModelError(
                            modelName,
                            $"{modelName} must be an integer.");

                        isSuccessfully = false;
                    }

                    return coord;
                })
                .ToArray();

            if(isSuccessfully)
            {
                bindingContext.Result = ModelBindingResult.Success(
                    new Point
                    {
                        X = coords[0],
                        Y = coords[1],
                        Z = coords[2]
                    }
                );
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}
