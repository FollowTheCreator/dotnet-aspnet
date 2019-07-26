using ITechart.DotNet.AspNet.CustomModelBinder.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace ITechart.DotNet.AspNet.CustomModelBinder.Infrastructure.Binders
{
    public class PersonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext
                .ValueProvider
                .GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.ModelState.TryAddModelError(
                           modelName,
                           $"{modelName} must exist.");

                bindingContext.Result = ModelBindingResult.Failed();

                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value) || value.Length != 48)
            {
                bindingContext.ModelState.TryAddModelError(
                           modelName,
                           $"{modelName} must have a value in base64 of Guid format.");

                bindingContext.Result = ModelBindingResult.Failed();

                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(value.DecodeToGuid());

            return Task.CompletedTask;
        }
    }
}
