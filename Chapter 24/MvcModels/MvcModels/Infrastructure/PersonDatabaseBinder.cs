using MvcModels.Controllers;
using System;
using System.Web.Mvc;

namespace MvcModels.Infrastructure
{
    public class PersonDatabaseBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = (bindingContext.ValueProvider.GetValue(bindingContext.ModelName).RawValue as string[])[0];

            return HomeController.Persons[Int32.Parse(value)];
        }
    }
}