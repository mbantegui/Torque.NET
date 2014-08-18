using System;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using TorqueServer.Domain.Uploads;

namespace TorqueServer.Web.Models
{
    public class RawUploadModelBinder : IModelBinder
    {
        private static readonly Type TargetType = typeof(RawUpload);

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != TargetType)
            {
                return false;
            }

            bindingContext.Model = new RawUpload();

            return true;
        }
    }
}