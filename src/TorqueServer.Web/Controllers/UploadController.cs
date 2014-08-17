using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TorqueServer.Web.Controllers
{
    public class UploadController : ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}