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

            if (GetQueryStringValue<int>(queryString, "v") != 7)
            {
                return false;
            }

            bindingContext.Model = new RawUpload
            {
                EmailAddress = GetQueryStringValue<string>(queryString, "eml"),
                SessionID = GetQueryStringValue<long>(queryString, "session"),
                CorrelationID = GetQueryStringValue<string>(queryString, "id"),
                RecordedOn = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Add(TimeSpan.FromMilliseconds(GetQueryStringValue<long>(queryString, "time"))),
                Readings = queryString.Where(pair => pair.Key.StartsWith("k"))
                                      .ToDictionary(pair => pair.Key, pair => TryConvert<double>(pair.Value))
            };

            return true;
        }

        private static T GetQueryStringValue<T>(IDictionary<string, string> values, string key)
        {
            string value;
            return values.TryGetValue(key, out value) ? TryConvert<T>(value) : default(T);
        }

        private static T TryConvert<T>(string value)
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }
    }
}