using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            var queryString = actionContext.Request.GetQueryNameValuePairs()
                                           .ToDictionary(pair => pair.Key, pair => pair.Value);

            bindingContext.Model = new RawUpload
            {
                EmailAddress = GetQueryStringValue(queryString, "eml")
            };

            return true;
        }

        private static string GetQueryStringValue(IDictionary<string, string> values, string key)
        {
            string value;
            return values.TryGetValue(key, out value) ? value : null;
        }

    }
}