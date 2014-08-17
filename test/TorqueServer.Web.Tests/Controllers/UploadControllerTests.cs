using System.Net.Http;
using TorqueServer.Web.Controllers;
using Xunit;

namespace TorqueServer.Web.Tests.Controllers
{
    public static class UploadControllerTests
    {
        public class GetFacts
        {
            [Fact]
            public void ReturnsAnHttpResponseMesssageForTheRequest()
            {
                // Arrange
                var httpRequestMessage = new HttpRequestMessage();
                var uploadController = new UploadController { Request = httpRequestMessage };

                // Act
                var httpResponseMessage = uploadController.Get();

                // Assert
                Assert.Equal(httpResponseMessage.RequestMessage, httpRequestMessage);
            }
        }
    }
}