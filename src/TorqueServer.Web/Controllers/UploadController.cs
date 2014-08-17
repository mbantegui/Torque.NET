using System.Net;
using System.Net.Http;
using System.Web.Http;
using TorqueServer.Domain.Uploads;

namespace TorqueServer.Web.Controllers
{
    public class UploadController : ApiController
    {
        private readonly IUploadTracker _uploadTracker;

        public UploadController(IUploadTracker uploadTracker)
        {
            _uploadTracker = uploadTracker;
        }

        public HttpResponseMessage Get(RawUpload rawUpload)
        {
            _uploadTracker.Save(rawUpload);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}