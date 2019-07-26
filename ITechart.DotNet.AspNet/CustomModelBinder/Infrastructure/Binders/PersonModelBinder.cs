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

            var valueProviderResult = bindingContext
                .ValueProvider
                .GetValue("id");

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(value.DecodeToGuid());

            return Task.CompletedTask;
        }
    }
}
